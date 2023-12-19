using Microsoft.Extensions.DependencyInjection;
using RuleBuilderInfra.Domain.Entities;
using RuleBuilderInfra.Persistence.Repositories.Contracts;
using RuleBuilderInfra.Persistence.Repositories.Contracts.RuleEngine;
using RuleBuilderInfra.Persistence.Repositories.Implementations.RuleEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RuleBuilderInfra.Persistence.Repositories.Implementations.RuleEngine
{
    public class RuleBuilderEngineRepo<T> : BaseRepository, IRuleBuilderEngineRepo<T>
    {
        private Expression? bodyQuery = null;
        private Expression? leftQuery = null;
        private Stack<QueueEntity<T>>? queries;
        private readonly IFieldOperatorJoiningRepository _fieldOperatorJoiningRepository;
        private readonly IFieldTypesRepository _fieldTypesRepository;
        private Dictionary<string, string> _keyValuePairs;
        private List<FieldTypesEntity> _fieldTypesEntities;

        public RuleBuilderEngineRepo(RuleEngineContext ruleEngineContext,
                                    IFieldOperatorJoiningRepository fieldOperatorJoiningRepository,
                                    IFieldTypesRepository fieldTypesRepository) : base(ruleEngineContext)
        {
            this._fieldOperatorJoiningRepository = fieldOperatorJoiningRepository;
            _fieldTypesRepository = fieldTypesRepository;

        }

        public Func<T, bool> GenerateQueryBuilder(RuleEntity ruleEntity)
        {
            _fieldTypesEntities = _fieldTypesRepository.GetFieldTypesAsync().Result;
            GetPropertyPairs(ruleEntity.EntityCategoryCode, ruleEntity.EntityCode);
            var conditions = ruleEntity.Conditions;
            FillingQueueWithQueries(conditions);
            var QueryBuilder = GenerateQuery();

            return QueryBuilder;
        }

        private Func<T, bool> GenerateQuery()
        {
            queries.GroupBy(z => z.groupID).ToList().ForEach(z =>
            {
                var groupedQueueEntities = new Queue<QueueEntity<T>>();
                z.Select(z => z as QueueEntity<T>).ToList().ForEach(item =>
                {
                    groupedQueueEntities.Enqueue(item);
                });

                bodyQuery = null;
                var rightQuery = GeneratingExpressionFromQueue<T>(groupedQueueEntities);
                leftQuery = leftQuery == null ? rightQuery : Expression.Or(rightQuery, leftQuery);
            });


            var param = Expression.Parameter(typeof(T), "P");
            leftQuery = (Expression)new ParameterReplacer(param).Visit(leftQuery);
            var queryLambda = Expression.Lambda<Func<T, bool>>(leftQuery, param).Compile();
            return queryLambda;
        }

        private string GetPropertyTypeByName(string propertyName)
        {
            var propertyType = _keyValuePairs.SingleOrDefault(kv => kv.Key.ToLower() == propertyName.ToLower());
            if (propertyType.Value == null)
                throw new ArgumentNullException(nameof(GetPropertyTypeByName));
            return _fieldTypesEntities.SingleOrDefault(property => property.FieldType.ToLower().Equals(propertyType.Value.ToLower()))!.FieldTypeCode;
        }

        private void FillingQueueWithQueries(List<ConditionRuleEntity> conditionRuleEntities, int parentId = 0)
        {
            Expression<Func<T, bool>> resultQuery = null;
            queries = queries == null ? new Stack<QueueEntity<T>>() : queries;
            var param = Expression.Parameter(typeof(T), "P");

            foreach (var item in conditionRuleEntities)
            {
                if (item.conditions != null && item.conditions.Count > 0)
                    FillingQueueWithQueries(item.conditions, item.Id);
                item.ParentId = parentId == 0 ? null : parentId;
                Expression query = null;
                var fieldTypeCode = GetPropertyTypeByName(item.PropertyName);
                GenerateExpressionBasedOnFieldTypes(item,fieldTypeCode);
                //var fieldTypeCode = _fieldTypesEntities.Where(z => z.FieldType == item.PropertyName).FirstOrDefault();


                Type fieldTypeOf = GetTypeOfProperty(fieldTypeCode);
                if (fieldTypeCode == "ST")
                {
                    if (item.Operator == "Eq")
                        query = ExpressionDynamicBuilder.CreateEqualBinaryExpression<T>(item.PropertyName, item.Value, fieldTypeOf);
                    else if (item.Operator == "NEq")
                        query = ExpressionDynamicBuilder.NotEqualBinaryExpression<T>(item.PropertyName, item.Value, fieldTypeOf);
                    else if (item.Operator == "Stw")
                        query = ExpressionDynamicBuilder.StartsWithExpression<T>(item.PropertyName, item.Value, fieldTypeOf);
                    else if (item.Operator == "Cte")
                        query = ExpressionDynamicBuilder.ContainsExpression<T>(item.PropertyName, item.Value, fieldTypeOf);
                }
                else if (fieldTypeCode == "Int32" || fieldTypeCode == "Int64")
                {
                    if (item.Operator == "Eq")
                        query = ExpressionDynamicBuilder.CreateEqualBinaryExpression<T>(item.PropertyName, item.Value, fieldTypeOf);
                    else if (item.Operator == "NEq")
                        query = ExpressionDynamicBuilder.NotEqualBinaryExpression<T>(item.PropertyName, item.Value, fieldTypeOf);
                    else if (item.Operator == "Gt")
                        query = ExpressionDynamicBuilder.GreaterThanBinaryExpression<T>(item.PropertyName, item.Value, fieldTypeOf);
                    else if (item.Operator == "Gte")
                        query = ExpressionDynamicBuilder.GreaterThanEqualBinaryExpression<T>(item.PropertyName, item.Value, fieldTypeOf);
                    else if (item.Operator == "Lt")
                        query = ExpressionDynamicBuilder.LessThanBinaryExpression<T>(item.PropertyName, item.Value, fieldTypeOf);
                    else if (item.Operator == "Lte")
                        query = ExpressionDynamicBuilder.LessThanEqualBinaryExpression<T>(item.PropertyName, item.Value, fieldTypeOf);
                }


                queries.Push(new QueueEntity<T>
                {
                    query = query,
                    groupID = item.ParentId != null ? item.ParentId.Value : item.Id,
                    condition = item.ConditionCode
                });
            }
        }

        private void GenerateExpressionBasedOnFieldTypes(ConditionRuleEntity item,string fielTypeCode)
        {
            var fieldTypesAllowedForCallingMethods = _fieldOperatorJoiningRepository.GetFieldOperatorByCode(fielTypeCode).Result;
            if (fieldTypesAllowedForCallingMethods == null)
                throw new NullReferenceException();
            if (!fieldTypesAllowedForCallingMethods.Any(z => z.OperatorTypeCode == item.Operator))
                throw new ArgumentOutOfRangeException();
        }

        private Type GetTypeOfProperty(string code)
        {
            var fileType = this._fieldTypesRepository.GetFieldTypesEntityByCodeAsync(code).Result;
            if (fileType.FieldTypeCode == "ST")
                return typeof(String);
            else if (fileType.FieldTypeCode == "Int32")
                return typeof(Int32);
            else if (fileType.FieldTypeCode == "Int64")
                return typeof(Int64);
            else 
                throw new ArgumentOutOfRangeException(nameof(GetTypeOfProperty));    
        }


        private Expression GeneratingExpressionFromQueue<T>(Queue<QueueEntity<T>> queueEntities)
        {

            var lastQuery = queueEntities.Dequeue();
            if (queueEntities.Count > 0)
                GeneratingExpressionFromQueue(queueEntities);
            if (lastQuery.condition == "OR")
                bodyQuery = bodyQuery == null ? lastQuery.query : Expression.Or(bodyQuery, lastQuery.query);
            else if (lastQuery.condition == "AND")
                bodyQuery = bodyQuery == null ? lastQuery.query : Expression.AndAlso(bodyQuery, lastQuery.query);
            else
                bodyQuery = bodyQuery == null ? lastQuery.query : Expression.AndAlso(bodyQuery, lastQuery.query);
            return bodyQuery;
        }


        public async Task<Dictionary<string, string>> GetPropertyPairs(string assemblyName, string entityTypeCode)
        {
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            var type = CommonUtility.LoadAssemblyType(assemblyName, entityTypeCode);
            _keyValuePairs = CommonUtility.GetKeyValuePairs(type);
            return keyValuePairs;
        }
    }
}
