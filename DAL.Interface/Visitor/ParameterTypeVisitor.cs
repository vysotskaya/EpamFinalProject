using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace DAL.Interface.Visitor
{
    public class ParameterTypeVisitor<TFrom, TTo> : ExpressionVisitor
    {

        private Dictionary<string, ParameterExpression> convertedParameters;
        private Expression<Func<TFrom, bool>> expression;

        public ParameterTypeVisitor(Expression<Func<TFrom, bool>> expresionToConvert)
        {
            //for each parameter in the original expression creates a new parameter with the same name but with changed type 
            convertedParameters = expresionToConvert.Parameters
                .ToDictionary(
                    x => x.Name,
                    x => Expression.Parameter(typeof(TTo), x.Name)
                );

            expression = expresionToConvert;
        }

        public Expression<Func<TTo, bool>> Convert()
        {
            return (Expression<Func<TTo, bool>>)Visit(expression);
        }

        //handles Properties and Fields accessors 
        protected override Expression VisitMember(MemberExpression node)
        {
            //we want to replace only the nodes of type TFrom
            //so we can handle expressions of the form x=> x.Property.SubProperty
            //in the expression x=> x.Property1 == 6 && x.Property2 == 3
            //this replaces         ^^^^^^^^^^^         ^^^^^^^^^^^            
            if (node.Member.DeclaringType == typeof(TFrom))
            {
                //gets the memberinfo from type TTo that matches the member of type TFrom
                var memeberInfo = GetMemberInfoByDalClass(typeof (TFrom).ToString(), node);
                //this will actually call the VisitParameter method in this class
                var newExp = Visit(node.Expression);
                return Expression.MakeMemberAccess(newExp, memeberInfo);
            }
            else
            {
                return base.VisitMember(node);
            }
        }

        // this will be called where ever we have a reference to a parameter in the expression
        // for ex. in the expression x=> x.Property1 == 6 && x.Property2 == 3
        // this will be called twice     ^                   ^
        protected override Expression VisitParameter(ParameterExpression node)
        {
            var newParameter = convertedParameters[node.Name];
            return newParameter;
        }

        //this will be the first Visit method to be called
        //since we're converting LamdaExpressions
        protected override Expression VisitLambda<T>(Expression<T> node)
        {
            //visit the body of the lambda, this will Traverse the ExpressionTree 
            //and recursively replace parts of the expresion we for witch we have matching Visit methods 
            var newExp = Visit(node.Body);

            //this will create the new expression            
            return Expression.Lambda(newExp, convertedParameters.Select(x => x.Value));
        }

        private MemberInfo GetMemberInfoByDalClass(string dalClassInfo, MemberExpression node)
        {
            switch (typeof(TFrom).ToString())
            {
                case "DAL.Interface.DTO.DalRole":
                    return node.Member.Name == "Id"
                        ? typeof(TTo).GetMember("RoleId").First()
                        : typeof(TTo).GetMember("RoleName").First();
                case "DAL.Interface.DTO.DalBid":
                    if (node.Member.Name == "Id")
                    {
                        return typeof (TTo).GetMember("BidId").First();
                    }
                    if (node.Member.Name == "UserRefId")
                    {
                        return typeof(TTo).GetMember("UserRefId").First();
                    }
                    if (node.Member.Name == "LogRefId")
                    {
                        return typeof(TTo).GetMember("LogRefId").First();
                    }
                    if (node.Member.Name == "BidAmount")
                    {
                        return typeof(TTo).GetMember("BidAmount").First();
                    }
                    return typeof(TTo).GetMember(node.Member.Name).First();
                case "DAL.Interface.DTO.DalCategory":
                    if (node.Member.Name == "Id")
                    {
                        return typeof(TTo).GetMember("CategoryId").First();
                    }
                    return typeof(TTo).GetMember(node.Member.Name).First();
                case "DAL.Interface.DTO.DalLot":
                    if (node.Member.Name == "Id")
                    {
                        return typeof(TTo).GetMember("LotId").First();
                    }
                    return typeof(TTo).GetMember(node.Member.Name).First();
                case "DAL.Interface.DTO.DalRequest":
                    if (node.Member.Name == "Id")
                    {
                        return typeof(TTo).GetMember("RequestId").First();
                    }
                    if (node.Member.Name == "IsConfirm")
                    {
                        return typeof(TTo).GetMember("ToConfirm").First();
                    }
                    return typeof(TTo).GetMember(node.Member.Name).First();
                case "DAL.Interface.DTO.DalSection":
                    if (node.Member.Name == "Id")
                    {
                        return typeof(TTo).GetMember("SectionId").First();
                    }
                    return typeof(TTo).GetMember(node.Member.Name).First();
                case "DAL.Interface.DTO.DalUser":
                    if (node.Member.Name == "Id")
                    {
                        return typeof(TTo).GetMember("UserId").First();
                    }
                    return typeof(TTo).GetMember(node.Member.Name).First();
                default:
                    return typeof(TTo).GetMember(node.Member.Name).First();
            }
        }
    }
}
