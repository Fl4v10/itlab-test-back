using System;

namespace testeItLab.domain.Exceptions
{
    public class NotFoundEntityException<TEntity> : Exception where TEntity : class, new()
    {
        // TODO: Colocar num arquivo de configuração e tradução. e.g. .RESX
        public NotFoundEntityException() : base("Entity not found!")
        {
            EntityType = typeof(TEntity);
        }

        public Type EntityType { get; }
    }
}
