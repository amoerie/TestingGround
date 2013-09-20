using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using TestingGround.Core.Domain.Internal.Bases;

namespace TestingGround.Core.Infrastructure.PropertyIntercepting
{
    public class FilteredCollection <TEntity> : ICollection<TEntity> where TEntity : Entity
    {
        private readonly DbCollectionEntry _dbCollectionEntry;
        private readonly Expression<Func<TEntity, Boolean>> _filter;
        private ICollection<TEntity> _collection;

        public FilteredCollection(ICollection<TEntity> collection, DbCollectionEntry dbCollectionEntry)
        {
            _filter = entity => !entity.Deleted;
            _dbCollectionEntry = dbCollectionEntry;
            _collection = collection;
        }
        
        private ICollection<TEntity> Entities
        {
            get
            {
                if (_dbCollectionEntry.IsLoaded == false && _collection == null)
                {
                    IQueryable<TEntity> query = _dbCollectionEntry.Query().Cast<TEntity>().Where(_filter);
                    _dbCollectionEntry.CurrentValue = this;
                    _collection = query.ToList();

                    object internalCollectionEntry =
                        _dbCollectionEntry.GetType()
                            .GetField("_internalCollectionEntry", BindingFlags.NonPublic | BindingFlags.Instance)
                            .GetValue(_dbCollectionEntry);
                    object relatedEnd =
                        internalCollectionEntry.GetType()
                            .BaseType.GetField("_relatedEnd", BindingFlags.NonPublic | BindingFlags.Instance)
                            .GetValue(internalCollectionEntry);
                    relatedEnd.GetType()
                        .GetField("_isLoaded", BindingFlags.NonPublic | BindingFlags.Instance)
                        .SetValue(relatedEnd, true);
                }
                return _collection;
            }
        }

        #region ICollection<T> Members

        void ICollection<TEntity>.Add(TEntity item)
        {
            Entities.Add(item);
        }

        void ICollection<TEntity>.Clear()
        {
            Entities.Clear();
        }

        Boolean ICollection<TEntity>.Contains(TEntity item)
        {
            return Entities.Contains(item);
        }

        void ICollection<TEntity>.CopyTo(TEntity[] array, Int32 arrayIndex)
        {
            Entities.CopyTo(array, arrayIndex);
        }

        Int32 ICollection<TEntity>.Count
        {
            get
            {
                return
                    /*_dbCollectionEntry.IsLoaded ? Entities.Count : _dbCollectionEntry.Query().Cast<TEntity>().Count(_filter);*/
                    Entities.Count;
            }
        }

        Boolean ICollection<TEntity>.IsReadOnly
        {
            get
            {
                return Entities.IsReadOnly;
            }
        }

        Boolean ICollection<TEntity>.Remove(TEntity item)
        {
            return Entities.Remove(item);
        }

        #endregion

        #region IEnumerable<T> Members

        IEnumerator<TEntity> IEnumerable<TEntity>.GetEnumerator()
        {
            return Entities.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ( ( this as IEnumerable<TEntity> ).GetEnumerator() );
        }

        #endregion
    }
}