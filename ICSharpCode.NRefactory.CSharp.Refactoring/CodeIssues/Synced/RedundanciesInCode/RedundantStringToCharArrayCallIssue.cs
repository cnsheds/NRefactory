//
// RedundantStringToCharArrayCallIssue.cs
//
// Author:
//       Mike Krüger <mkrueger@xamarin.com>
//
// Copyright (c) 2013 Xamarin Inc. (http://xamarin.com)
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CodeFixes;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.Text;
using System.Threading;
using ICSharpCode.NRefactory6.CSharp.Refactoring;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;
using Microsoft.CodeAnalysis.Formatting;
using Microsoft.CodeAnalysis.FindSymbols;

namespace ICSharpCode.NRefactory6.CSharp.Refactoring
{
	[DiagnosticAnalyzer]
	[NRefactoryCodeDiagnosticAnalyzer(AnalysisDisableKeyword = "RedundantStringToCharArrayCall")]
	public class RedundantStringToCharArrayCallIssue : GatherVisitorCodeIssueProvider
	{
		internal const string DiagnosticId  = "RedundantStringToCharArrayCallIssue";
		const string Description            = "Redundant 'string.ToCharArray()' call";
		const string MessageFormat          = "Redundant 'string.ToCharArray()' call";
		const string Category               = IssueCategories.RedundanciesInCode;

		static readonly DiagnosticDescriptor Rule = new DiagnosticDescriptor (DiagnosticId, Description, MessageFormat, Category, DiagnosticSeverity.Warning, true, "Redundant 'string.ToCharArray()' call");

		public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics {
			get {
				return ImmutableArray.Create(Rule);
			}
		}

		protected override CSharpSyntaxWalker CreateVisitor (SemanticModel semanticModel, Action<Diagnostic> addDiagnostic, CancellationToken cancellationToken)
		{
			return new GatherVisitor(semanticModel, addDiagnostic, cancellationToken);
		}

		class GatherVisitor : GatherVisitorBase<RedundantStringToCharArrayCallIssue>
		{
			public GatherVisitor(SemanticModel semanticModel, Action<Diagnostic> addDiagnostic, CancellationToken cancellationToken)
				: base (semanticModel, addDiagnostic, cancellationToken)
			{
			}

//			void AddProblem(AstNode replaceExpression, InvocationExpression invocationExpression)
//			{
//				var t = invocationExpression.Target as MemberReferenceExpression;
//				AddIssue(new CodeIssue (
//					t.DotToken.StartLocation,
//					t.MemberNameToken.EndLocation,
//					ctx.TranslateString(""),
//					ctx.TranslateString(""),
//					s => s.Replace(replaceExpression, t.Target.Clone())
//				) { IssueMarker = IssueMarker.GrayOut });
//			}
//
//			public override void VisitInvocationExpression(InvocationExpression invocationExpression)
//			{
//				base.VisitInvocationExpression(invocationExpression);
//
//				var t = invocationExpression.Target as MemberReferenceExpression;
//				if (t == null || t.MemberName != "ToCharArray")
//					return;
//				var rr = ctx.Resolve(t.Target);
//				if (!rr.Type.IsKnownType(KnownTypeCode.String))
//					return;
//				if (invocationExpression.Parent is ForeachStatement && invocationExpression.Role == Roles.Expression) {
//					AddProblem(invocationExpression, invocationExpression);
//					return;
//				}
//				var p = invocationExpression.Parent;
//				while (p is ParenthesizedExpression) {
//					p = p.Parent;
//				}
//				var idx = p as IndexerExpression;
//				if (idx != null) {
//					AddProblem(idx.Target, invocationExpression);
//					return;
//				}
//			}
		}
	}

	[ExportCodeFixProvider(RedundantStringToCharArrayCallIssue.DiagnosticId, LanguageNames.CSharp)]
	public class RedundantStringToCharArrayCallFixProvider : NRefactoryCodeFixProvider
	{
		public override IEnumerable<string> GetFixableDiagnosticIds()
		{
			yield return RedundantStringToCharArrayCallIssue.DiagnosticId;
		}

		public override async Task<IEnumerable<CodeAction>> GetFixesAsync(Document document, TextSpan span, IEnumerable<Diagnostic> diagnostics, CancellationToken cancellationToken)
		{
			var root = await document.GetSyntaxRootAsync(cancellationToken);
			var result = new List<CodeAction>();
			foreach (var diagonstic in diagnostics) {
				var node = root.FindNode(diagonstic.Location.SourceSpan);
				//if (!node.IsKind(SyntaxKind.BaseList))
				//	continue;
				var newRoot = root.RemoveNode(node, SyntaxRemoveOptions.KeepNoTrivia);
				result.Add(CodeActionFactory.Create(node.Span, diagonstic.Severity, "Remove redundant 'string.ToCharArray()' call", document.WithSyntaxRoot(newRoot)));
			}
			return result;
		}
	}
}