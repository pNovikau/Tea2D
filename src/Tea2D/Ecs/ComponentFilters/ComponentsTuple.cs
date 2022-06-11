using System;
using Tea2D.Ecs.Components;

namespace Tea2D.Ecs.ComponentFilters;

public ref struct ComponentsTuple<TComponent, TComponent1>
    where TComponent : struct, IComponent<TComponent>
    where TComponent1 : struct, IComponent<TComponent1>
{
    public Span<TComponent> Component;
    public Span<TComponent1> Component1;

    public ComponentsTuple(Span<TComponent> component, Span<TComponent1> component1)
    {
        Component = component;
        Component1 = component1;
    }

    public void Deconstruct(out Span<TComponent> component, out Span<TComponent1> component1)
    {
        component = Component;
        component1 = Component1;
    }
}

public ref struct ComponentsTuple<TComponent, TComponent1, TComponent2>
    where TComponent : struct, IComponent<TComponent>
    where TComponent1 : struct, IComponent<TComponent1>
    where TComponent2 : struct, IComponent<TComponent2>
{
    public Span<TComponent> Component;
    public Span<TComponent1> Component1;
    public Span<TComponent2> Component2;

    public ComponentsTuple(
        Span<TComponent> component, 
        Span<TComponent1> component1,
        Span<TComponent2> component2)
    {
        Component = component;
        Component1 = component1;
        Component2 = component2;
    }

    public void Deconstruct(
        out Span<TComponent> component, 
        out Span<TComponent1> component1,
        out Span<TComponent2> component2)
    {
        component = Component;
        component1 = Component1;
        component2 = Component2;
    }
}

public unsafe ref struct ComponentsTuple<TComponent, TComponent1, TComponent2, TComponent3>
    where TComponent : struct, IComponent<TComponent>
    where TComponent1 : struct, IComponent<TComponent1>
    where TComponent2 : struct, IComponent<TComponent2>
    where TComponent3 : struct, IComponent<TComponent3>
{
    public Span<TComponent> Component;
    public Span<TComponent1> Component1;
    public Span<TComponent2> Component2;
    public Span<TComponent3> Component3;

    public ComponentsTuple(
        Span<TComponent> component, 
        Span<TComponent1> component1,
        Span<TComponent2> component2,
        Span<TComponent3> component3)
    {
        Component = component;
        Component1 = component1;
        Component2 = component2;
        Component3 = component3;
    }

    public void Deconstruct(
        out Span<TComponent> component, 
        out Span<TComponent1> component1,
        out Span<TComponent2> component2,
        out Span<TComponent3> component3)
    {
        component = Component;
        component1 = Component1;
        component2 = Component2;
        component3 = Component3;
    }
}