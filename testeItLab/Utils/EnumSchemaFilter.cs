using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Linq;
using System.Reflection;

namespace testeItLab.web.Utils
{
    public class EnumSchemaFilter : ISchemaFilter
    {
        public void Apply(Schema schema, SchemaFilterContext context)
        {
            if ((schema.Enum?.Count ?? 0) > 0)
            {
                return;
            }

            var properties = schema.Properties?.Where(prop => prop.Value.Enum?.Count > 0 || (prop.Value.Type == "array" && prop.Value.Items.Enum?.Count > 0));
            if (properties != null && properties.Any())
            {
                foreach (var prop in properties.ToList())
                {
                    var typeInfo = context.SystemType.GetTypeInfo();
                    var propertyInfo = typeInfo.DeclaredProperties.FirstOrDefault(x => x.Name.Equals(prop.Key, StringComparison.OrdinalIgnoreCase));

                    if (propertyInfo != null)
                    {
                        var enumType = propertyInfo.PropertyType;
                        var targetTypeDefinition = enumType.GetTypeInfo();
                        if (enumType.IsArray)
                        {
                            enumType = targetTypeDefinition.GetElementType();
                            targetTypeDefinition = enumType.GetTypeInfo();
                        }

                        if (targetTypeDefinition.IsGenericType && targetTypeDefinition.GetGenericTypeDefinition() == typeof(Nullable<>))
                        {
                            enumType = targetTypeDefinition.GenericTypeArguments[0];
                            targetTypeDefinition = enumType.GetTypeInfo();
                        }

                        if (targetTypeDefinition.IsEnum)
                        {

                            var enumSchema = prop.Value;

                            for (int i = 0; i < enumSchema.Enum.Count; i++)
                            {
                                enumSchema.Enum[i] = (enumSchema.Enum[i].ToString()).First().ToString().ToUpper() + (enumSchema.Enum[i].ToString()).Substring(1);
                            }

                            Schema typeEnumSchema = new Schema
                            {
                                Ref = $"#/definitions/{enumType.Name}"
                            };

                            if (prop.Value.Type == "array")
                            {
                                enumSchema = prop.Value.Items;
                                schema.Properties[prop.Key].Items = typeEnumSchema;
                            }
                            else
                                schema.Properties[prop.Key] = typeEnumSchema;


                            if (!context.SchemaRegistry.Definitions.ContainsKey(enumType.Name))
                                context.SchemaRegistry.Definitions.Add(enumType.Name, enumSchema);
                        }
                    }
                }
            }
        }
    }
}
