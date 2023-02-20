namespace Tea2D.SourceGenerators.ComponentFilters
{
    public static class ComponentFiltersClassBuilder
    {
        public static void Build(SourceCodeBuilder builder, string[] genericParameters)
        {
            var genericParametersString = string.Join(", ", genericParameters);

            builder.AppendLine("[global::System.CodeDom.Compiler.GeneratedCode(\"Tea2D.SourceGenerator\", \"0.0.1\")]");
            builder.AppendLineFormat("public sealed partial class ComponentsFilter<{0}>", genericParametersString);

            using var _ = builder.Scope();

            AppendConstructor(builder, genericParameters);
            AppendGetEnumerator(builder, genericParameters);
            AppendIEnumeratorGetEnumerator(builder, genericParameters);
            AppendEnumeratorStruct(builder, genericParameters);
        }

        private static void AppendConstructor(SourceCodeBuilder builder, string[] genericParameters)
        {
            builder.AppendLine("public ComponentsFilter(global::Tea2D.Ecs.Managers.IEntityManager entityManager, global::Tea2D.Ecs.Managers.IComponentManager componentManager) : base(entityManager, componentManager,");

            using (builder.Indent())
            {
                for (var i = 0; i < genericParameters.Length; i++)
                {
                    builder.AppendFormat("global::Tea2D.Ecs.Components.IComponent<{0}>.ComponentType", genericParameters[i]);
                    builder.AppendLine(genericParameters.Length - 1 != i ? "," : ")");
                }

                using (builder.Scope())
                {
                }
            }

            builder.AppendLine();
        }

        private static void AppendGetEnumerator(SourceCodeBuilder builder, string[] genericParameters)
        {
            builder.AppendLine("public Enumerator GetEnumerator() => new(this);");
            builder.AppendLine();
        }

        private static void AppendIEnumeratorGetEnumerator(SourceCodeBuilder builder, string[] genericParameters)
        {
            builder.AppendLine("global::System.Collections.IEnumerator global::System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();");
            builder.AppendLine();
        }

        private static void AppendEnumeratorStruct(SourceCodeBuilder builder, string[] genericParameters)
        {
            var genericParametersString = string.Join(", ", genericParameters);

            builder.AppendLine("public struct Enumerator : global::System.Collections.IEnumerator, global::System.IDisposable");
            using var __ = builder.Scope();

            builder.AppendLineFormat("private readonly ComponentsFilter<{0}> _componentFilter;", genericParametersString);
            builder.AppendLine();

            builder.AppendLine("private int _index = -1;");
            builder.AppendLine();

            AppendEnumeratorStructConstructor(builder, genericParameters);
            AppendEnumeratorStructMoveNext(builder, genericParameters);
            AppendEnumeratorStructReset(builder, genericParameters);
            AppendEnumeratorStructCurrent(builder, genericParameters);
            AppendEnumeratorStructIEnumeratorCurrent(builder, genericParameters);
            AppendEnumeratorStructDispose(builder, genericParameters);
        }

        private static void AppendEnumeratorStructIEnumeratorCurrent(SourceCodeBuilder builder, string[] genericParameters)
        {
            builder.AppendLine("object global::System.Collections.IEnumerator.Current => throw new global::System.NotImplementedException();");
            builder.AppendLine();
        }

        private static void AppendEnumeratorStructDispose(SourceCodeBuilder builder, string[] genericParameters)
        {
            builder.AppendLine("public void Dispose() => Reset();");
        }

        private static void AppendEnumeratorStructConstructor(SourceCodeBuilder builder, string[] genericParameters)
        {
            var genericParametersString = string.Join(", ", genericParameters);

            builder.AppendLineFormat("public Enumerator(ComponentsFilter<{0}> componentFilter) : this()", genericParametersString);
            using (builder.Scope())
            {
                builder.AppendLine("_componentFilter = componentFilter;");
            }

            builder.AppendLine();
        }

        private static void AppendEnumeratorStructMoveNext(SourceCodeBuilder builder, string[] genericParameters)
        {
            builder.AppendLine("public bool MoveNext()");
            using (builder.Scope())
            {
                builder.AppendLine("if (_index == _componentFilter.EntitiesIds.Count - 1)");
                using (builder.Indent())
                    builder.AppendLine("return false;");

                builder.AppendLine();
                builder.AppendLine("_index++;");
                builder.AppendLine("return true;");
            }

            builder.AppendLine();
        }

        private static void AppendEnumeratorStructReset(SourceCodeBuilder builder, string[] genericParameters)
        {
            builder.AppendLine("public void Reset() => _index = -1;");
            builder.AppendLine();
        }

        private static void AppendEnumeratorStructCurrent(SourceCodeBuilder builder, string[] genericParameters)
        {
            var genericParametersString = string.Join(", ", genericParameters);

            builder.AppendLineFormat("public global::Tea2D.Ecs.ComponentFilters.ComponentsTuple<{0}> Current", genericParametersString);
            using (builder.Scope())
            {
                builder.AppendLine("get");
                using (builder.Scope())
                {
                    builder.AppendLine("ref var entity = ref _componentFilter.EntityManager.Get(_componentFilter.EntitiesIds[_index]);");
                    builder.AppendLine();

                    for (var i = 0; i < genericParameters.Length; i++)
                    {
                        builder.AppendLineFormat("var componentId{0} = entity.Components[global::Tea2D.Ecs.Components.IComponent<{1}>.ComponentType];", i, genericParameters[i]);
                        builder.AppendLineFormat("var component{0} = _componentFilter.ComponentManager.GetComponentAsSpan<{1}>(componentId{0});", i, genericParameters[i]);
                        builder.AppendLine();
                    }

                    builder.AppendFormat("return new global::Tea2D.Ecs.ComponentFilters.ComponentsTuple<{0}>(_componentFilter.EntitiesIds[_index], ", genericParametersString);
                    for (var i = 0; i < genericParameters.Length; i++)
                    {
                        builder.Append("component");
                        builder.Append(i);

                        if (genericParameters.Length - 1 != i)
                            builder.Append(",");
                    }

                    builder.AppendLine(");");
                }
            }

            builder.AppendLine();
        }
    }
}