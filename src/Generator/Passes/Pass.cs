﻿
namespace CppSharp.Passes
{
    /// <summary>
    /// Used to provide different types of code transformation on a module
    /// declarations and types before the code generation process is started.
    /// </summary>
    public abstract class TranslationUnitPass : AstVisitor
    {
        public Driver Driver { get; set; }
        public Library Library { get; set; }

        public virtual bool VisitLibrary(Library library)
        {
            foreach (var unit in library.TranslationUnits)
                VisitTranslationUnit(unit);

            return true;
        }

        public virtual bool VisitTranslationUnit(TranslationUnit unit)
        {
            if (unit.Ignore)
                return false;

            if (unit.IsSystemHeader)
                return false;

            VisitDeclarationContext(unit);

            return true;
        }

        public override bool VisitDeclaration(Declaration decl)
        {
            return !IsDeclExcluded(decl);
        }

        bool IsDeclExcluded(Declaration decl)
        {
            var type = this.GetType();
            return decl.ExcludeFromPasses.Contains(type);
        }
    }
}
