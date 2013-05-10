using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace AlisFirst.Models
{ 
    public class CheckListItemRepository : ICheckListItemRepository
    {
        AlisFirstContext context = new AlisFirstContext();

        public IQueryable<CheckListItem> All
        {
            get { return context.CheckListItems; }
        }

        public IQueryable<CheckListItem> AllIncluding(params Expression<Func<CheckListItem, object>>[] includeProperties)
        {
            IQueryable<CheckListItem> query = context.CheckListItems;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public CheckListItem Find(int id)
        {
            return context.CheckListItems.Find(id);
        }

        public void InsertOrUpdate(CheckListItem checklistitem)
        {
            if (checklistitem.CheckListItemID == default(int)) {
                // New entity
                context.CheckListItems.Add(checklistitem);
            } else {
                // Existing entity
                context.Entry(checklistitem).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var checklistitem = context.CheckListItems.Find(id);
            context.CheckListItems.Remove(checklistitem);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Dispose() 
        {
            context.Dispose();
        }
    }

    public interface ICheckListItemRepository : IDisposable
    {
        IQueryable<CheckListItem> All { get; }
        IQueryable<CheckListItem> AllIncluding(params Expression<Func<CheckListItem, object>>[] includeProperties);
        CheckListItem Find(int id);
        void InsertOrUpdate(CheckListItem checklistitem);
        void Delete(int id);
        void Save();
    }
}