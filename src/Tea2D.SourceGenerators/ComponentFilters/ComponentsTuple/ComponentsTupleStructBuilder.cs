namespace Tea2D.SourceGenerators.ComponentFilters.ComponentsTuple
{
    public static class ComponentsTupleStructBuilder
    {
        public static void Build(SourceCodeBuilder builder, string[] genericParameters)
        {
            var genericParametersString = string.Join(", ", genericParameters);

            builder.AppendLine("[global::System.CodeDom.Compiler.GeneratedCode(\"Tea2D.SourceGenerator\", \"0.0.1\")]");
            builder.AppendLineFormat("public ref struct ComponentsTuple<{0}>", genericParametersString);

            using (builder.Indent())
            {
                foreach (var genericParameter in genericParameters)
                {
                    builder.AppendLineFormat("where {0} : struct, global::Tea2D.Ecs.Components.IComponent<{0}>", genericParameter);
                }
            }

            using var _ = builder.Scope();

            AppendMembers(builder, genericParameters);
            AppendConstructor(builder, genericParameters);
            AppendDeconstruct(builder, genericParameters);
        }

        private static void AppendMembers(SourceCodeBuilder builder, string[] genericParameters)
        {
            builder.AppendLine("public int EntityId;");

            for (var i = 0; i < genericParameters.Length; i++)
            {
                builder.AppendLineFormat("public global::CommunityToolkit.HighPerformance.Ref<{0}> Component{1};", genericParameters[i], i);
            }

            builder.AppendLine();
        }

        private static void AppendConstructor(SourceCodeBuilder builder, string[] genericParameters)
        {
            builder.AppendLine("public ComponentsTuple(");
            using (builder.Indent())
            {
                builder.AppendLine("int entityId,");

                for (var i = 0; i < genericParameters.Length; i++)
                {
                    builder.AppendFormat("global::CommunityToolkit.HighPerformance.Ref<{0}> component{1}", genericParameters[i], i);
                    builder.AppendLine(genericParameters.Length - 1 != i ? "," : ")");
                }
            }

            using (builder.Scope())
            {
                builder.AppendLine("EntityId = entityId;");
                builder.AppendLine();

                for (var i = 0; i < genericParameters.Length; i++)
                {
                    builder.AppendLineFormat("Component{0} = component{0};", i);
                }
            }

            builder.AppendLine();
        }

        private static void AppendDeconstruct(SourceCodeBuilder builder, string[] genericParameters)
        {
            builder.AppendLine("public void Deconstruct(");
            using (builder.Indent())
            {
                builder.AppendLine("out int entityId,");

                for (var i = 0; i < genericParameters.Length; i++)
                {
                    builder.AppendFormat("out global::CommunityToolkit.HighPerformance.Ref<{0}> component{1}", genericParameters[i], i);
                    builder.AppendLine(genericParameters.Length - 1 != i ? "," : ")");
                }
            }

            using (builder.Scope())
            {
                builder.AppendLine("entityId = EntityId;");
                builder.AppendLine();

                for (var i = 0; i < genericParameters.Length; i++)
                {
                    builder.AppendLineFormat("component{0} = Component{0};", i);
                }
            }
        }
    }
}