namespace TBBProject.Core.Common
{
    using System.Diagnostics;
    [DebuggerStepThrough]
    public struct Validation<T>
    {
        public readonly string Name;
        public readonly T Value;

        public Validation(string name, T value)
        {
            Name = name;
            Value = value;
        }

        public Validation<T> Is => this;
    }
}
