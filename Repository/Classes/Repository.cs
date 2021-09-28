using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository.Classes
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationContext context;
        private DbSet<T> entities;
        string errorMessage = string.Empty;

        public Repository(ApplicationContext context)
        {
            this.context = context;
            entities = context.Set<T>();
        }
        public IEnumerable<T> GetAll()
        {
            return entities.AsEnumerable();
        }

        public IQueryable<T> Get()
        {
            return entities;
        }
        //public T Get(long id)
        //{
        //    return entities.SingleOrDefault(s => s.ID == id);
        //}
        public void Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            context.SaveChanges();
        }

        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Update(entity);
            context.SaveChanges();
        }
        public void RemoveList(List<T> entityList)
        {
            if (entityList == null)
                throw new ArgumentNullException("entityList");
            else
            {
                entities.RemoveRange(entityList);
                context.SaveChanges();
            }
        }
        public void InsertList(List<T> entityList)
        {
            if (entityList == null)
                throw new ArgumentNullException("entityList");
            else
            {
                entities.AddRange(entityList);
                context.SaveChanges();
            }
        }
        public void UpdateList(List<T> entityList)
        {
            if (entityList == null)
                throw new ArgumentNullException("entityList");
            else
            {
                entities.UpdateRange(entityList);
                context.SaveChanges();
            }
        }
        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            context.SaveChanges();
        }
        public void Remove(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }
        public int SaveChanges(int UserId, string IPaddress, int TargetID)
        {
            var changeEntries = context.ChangeTracker.Entries().Where(x => x.State != EntityState.Unchanged).ToList();
            List<LogTable> logs = new List<LogTable>();
            foreach (var item in changeEntries)
            {
                var TableName = item.Entity.GetType().Name;
                switch (item.State)
                {
                    case EntityState.Added:
                        {
                            //--------------------------------------- Add on Log -------------------------------------------------------
                            LogTable log = new LogTable();
                            log.UserID = UserId;
                            log.IPAddress = IPaddress;
                            log.TargetID = TargetID;
                            log.TableName = TableName;
                            log.Operation = "New";
                            log.IsActive = true;
                            log.AddedDate = System.DateTime.UtcNow;
                            logs.Add(log);
                            break;
                        }

                    case EntityState.Modified:
                        {
                            //--------------------------------------- Add on Log -------------------------------------------------------
                            LogTable log = new LogTable();
                            log.UserID = UserId;
                            log.IPAddress = IPaddress;
                            log.TargetID = TargetID;
                            log.TableName = TableName;
                            log.Operation = "Updated";
                            log.IsActive = true;
                            log.AddedDate = System.DateTime.UtcNow;
                            //--------------------------------------- Log details ------------------------------------------------------
                            var logDetails = new List<LogData>();
                            var props = item.Entity.GetType().GetProperties().Where(x => x.PropertyType == typeof(int) || x.PropertyType == typeof(string) || x.PropertyType == typeof(DateTime));
                            try
                            {
                                foreach (var prop in props)
                                {
                                    var oldValue = item.OriginalValues[prop.Name]?.ToString();
                                    var newValue = item.CurrentValues[prop.Name]?.ToString();
                                    if (oldValue != newValue)
                                    {
                                        var detail = new LogData();
                                        detail.LogTableID = log.ID;
                                        detail.ColumnName = prop.Name.ToString();
                                        detail.OldData = item.OriginalValues[prop.Name]?.ToString();
                                        detail.NewData = item.CurrentValues[prop.Name]?.ToString();
                                        detail.AddedDate = System.DateTime.UtcNow;
                                        log.LogData.Add(detail);
                                    }
                                }
                                logs.Add(log);
                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }
                            break;
                        }
                    default:
                        break;
                }
            }
            context.Set<LogTable>().AddRange(logs);
            return context.SaveChanges();
        }
    }
}
