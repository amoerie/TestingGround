using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingGround.Core.Infrastructure.Utilities.Loading
{
    public interface IEntityLoadModel <TEntity>
    {
        TEntity Entity { get; set; }
        IEntityLoadModel<TEntity> BaseLoadModel { get; set; }
    }

    public class EntityLoadModel<TEntity, TProperty> : IEntityLoadModel<TEntity>
    {
        public TEntity Entity { get; set; }
        public TProperty NavigationProperty { get; set; }
        public IEntityLoadModel<TEntity> BaseLoadModel { get; set; }

        public override string ToString()
        {
            return string.Format("Entity: {0}, NavigationProperty: {1}, BaseLoadModel: {2}", Entity, NavigationProperty, BaseLoadModel);
        }
    }
}
