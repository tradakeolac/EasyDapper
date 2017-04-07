using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EasyDapper.Data.Dapper.Helpers;
using System.Linq.Expressions;
using EasyDapper.Data.Dapper.Tests.Data.Dummy;
using EasyDapper.Data.Dapper.Helpers.QueryParser;
using Moq;
using System.Linq;

namespace EasyDapper.Data.Dapper.Tests
{
    [TestClass]
    public class MethodCallExpressionParserTests
    {
        [TestMethod]
        public void Parser()
        {
            IMethodCallTranslator translator = new StringMethodCallTranslator();
            var mock = new Mock<IServiceResolver<IMethodCallTranslator>>();
            mock.Setup(m => m.Resolve(It.IsAny<Type>())).Returns(translator);

            IExpressionParserStrategy parser = new MethodCallExpressionParser(mock.Object);
            var collection = new QueryParameterCollection();
            Func<SimpleModel, bool> s = (b) => b.Name.Contains("A");
            var param = Expression.Parameter(typeof(SimpleModel), "s");
            Expression property = Expression.Property(param, "Name");
            Expression left = Expression.Call(property, typeof(string).GetMethod("Contains"), Expression.Constant("A"));

            parser.Parser(left, ExpressionType.And, ref collection);
            Assert.IsTrue(collection.Count > 0);
            Assert.AreEqual(collection[0].ToString(), "");
        }
    }
}
