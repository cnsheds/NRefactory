﻿// 
// CreateField.cs
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
	[NRefactoryCodeRefactoringProvider(Description = "Creates a field for a undefined variable.")]
	[ExportCodeRefactoringProvider("Create field", LanguageNames.CSharp)]
	public class CreateFieldAction : CodeRefactoringProvider
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
//		internal static bool IsInvocationTarget(AstNode node)
//		{
//			var invoke = node.Parent as InvocationExpression;
//			return invoke != null && invoke.Target == node;
//		}
//
//		internal static Expression GetCreatePropertyOrFieldNode(SemanticModel context)
//		{
//			return context.GetNode(n => n is IdentifierExpression || n is MemberReferenceExpression || n is NamedExpression) as Expression;
//		}
//
//		public async Task<IEnumerable<CodeAction>> GetRefactoringsAsync(Document document, TextSpan span, CancellationToken cancellationToken)
//		{
//			var expr = GetCreatePropertyOrFieldNode(context);
//			if (expr == null)
//				yield break;
//
//			if (expr is MemberReferenceExpression && !(((MemberReferenceExpression)expr).Target is ThisReferenceExpression))
//				yield break;
//
//			var propertyName = CreatePropertyAction.GetPropertyName(expr);
//			if (propertyName == null)
//				yield break;
//
//			if (IsInvocationTarget(expr))
//				yield break;
//			var statement = expr.GetParent<Statement>();
//			if (statement == null)
//				yield break;
//			if (!(context.Resolve(expr).IsError))
//				yield break;
//			var guessedType = TypeGuessing.GuessAstType(context, expr);
//			if (guessedType == null)
//				yield break;
//			var state = context.GetResolverStateBefore(expr);
//			if (state.CurrentMember == null || state.CurrentTypeDefinition == null)
//				yield break;
//			bool isStatic =  !(expr is NamedExpression) && (state.CurrentMember.IsStatic | state.CurrentTypeDefinition.IsStatic);
//
////			var service = (NamingConventionService)context.GetService(typeof(NamingConventionService));
////			if (service != null && !service.IsValidName(identifier.Identifier, AffectedEntity.Field, Modifiers.Private, isStatic)) { 
////				yield break;
////			}
//
//			yield return new CodeAction(context.TranslateString("Create field"), script => {
//				var decl = new FieldDeclaration {
//					ReturnType = guessedType,
//					Variables = { new VariableInitializer(propertyName) }
//				};
//				if (isStatic)
//					decl.Modifiers |= Modifiers.Static;
//				script.InsertWithCursor(context.TranslateString("Create field"), Script.InsertPosition.Before, decl);
//			}, expr.GetNodeAt(context.Location) ?? expr) { Severity = ICSharpCode.NRefactory.Refactoring.Severity.Error };
//		}

	}
}

