// 
// CreateLocalVariable.cs
//  
// Author:
//       Mike Krüger <mkrueger@novell.com>
// 
// Copyright (c) 2011 Novell, Inc (http://www.novell.com)
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
using System.Linq;
using System.Threading;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.CodeRefactorings;
using Microsoft.CodeAnalysis;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.Text;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using ICSharpCode.NRefactory6.CSharp.Refactoring;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Simplification;
using Microsoft.CodeAnalysis.Formatting;

namespace ICSharpCode.NRefactory6.CSharp.Refactoring
{
	[NRefactoryCodeRefactoringProvider(Description = "Creates a local variable for a undefined variable")]
	[ExportCodeRefactoringProvider("Create local variable", LanguageNames.CSharp)]
	public class CreateLocalVariableAction : CodeRefactoringProvider
	{
		public override async Task<IEnumerable<CodeAction>> GetRefactoringsAsync(CodeRefactoringContext context)
		{
			var document = context.Document;
			var span = context.Span;
			var cancellationToken = context.CancellationToken;
			var model = await document.GetSemanticModelAsync(cancellationToken);
			var root = await model.SyntaxTree.GetRootAsync(cancellationToken);
			return null;
		}
//		public async Task<IEnumerable<CodeAction>> GetRefactoringsAsync(Document document, TextSpan span, CancellationToken cancellationToken)
//		{
//			var identifier = context.GetNode<IdentifierExpression>();
//			if (identifier == null) {
//				yield break;
//			}
//			if (CreateFieldAction.IsInvocationTarget(identifier)) {
//				yield break;
//			}
//			var statement = context.GetNode<Statement>();
//			if (statement == null) {
//				yield break;
//			}
//
//			if (!(context.Resolve(identifier).IsError)) {
//				yield break;
//			}
//			var guessedType = TypeGuessing.GuessAstType(context, identifier);
//			if (guessedType == null) {
//				yield break;
//			}
//
//			yield return new CodeAction(context.TranslateString("Create local variable"), script => {
//				var initializer = new VariableInitializer(identifier.Identifier);
//				var decl = new VariableDeclarationStatement() {
//					Type = guessedType,
//					Variables = { initializer }
//				};
//				if (identifier.Parent is AssignmentExpression && ((AssignmentExpression)identifier.Parent).Left == identifier) {
//					initializer.Initializer = ((AssignmentExpression)identifier.Parent).Right.Clone();
//					if (!context.UseExplicitTypes)
//						decl.Type = new SimpleType("var");
//					script.Replace(statement, decl);
//				} else {
//					script.InsertBefore(statement, decl);
//				}
//			}, identifier) { Severity = ICSharpCode.NRefactory.Refactoring.Severity.Error };
//		}
	}
}

