﻿using Utilities;
using ViewModels;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BE;
using BLL.Interfaces;
using DAL.Interfaces;
using Utilities.Exceptions;

namespace BLL
{
    public class BaseBLL<TEntity, TViewModel> : IBLL<TViewModel>
        where TEntity : BaseEntity
        where TViewModel : BaseViewModel
    {
        #region Fields
        protected IDAL<TEntity> Dal;
        protected static readonly ILog Log = LogManager.GetLogger(GlobalValues.WebLoggerName);
        #endregion Fields


        #region Public Methods

        public virtual Guid Add(TViewModel viewModel)
        {
            try
            {
                if(this.IsValid(viewModel))
                {
                    TEntity entity;
                    entity = Mapper.Map<TViewModel, TEntity>(viewModel);
                    Guid result = this.Dal.Add(entity);
                    return result;
                }
                else
                {
                    throw new Exception(Messages.Generic_Error);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
          
        }

        public virtual bool Delete(Guid id)
        {
            try
            {
                bool result;
                result = this.Dal.Delete(id);     
                return result;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }

        public virtual IList<TViewModel> Get()
        {
            try
            {
                IList<TEntity> entities;
                entities = this.Dal.Get();
                return Mapper.Map<TEntity, TViewModel>(entities);
            }
            catch(Exception ex)
            {
                throw ex;
            }          
        }

        public virtual TViewModel GetById(Guid id)
        {
            try
            {
                TEntity entity;
                entity = this.Dal.GetById(id);
                return Mapper.Map<TEntity, TViewModel>(entity);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }


        public virtual bool Update(TViewModel viewModel)
        {
            try
            {
                if (this.IsValid(viewModel))
                {
                    TEntity newValues = Mapper.Map<TViewModel, TEntity>(viewModel);

                    return this.Dal.Update(newValues);
                }
                else
                {
                    throw new BusinessException(Messages.InvalidData);
                }
               
            }
            catch(BusinessException ex)
            {
                throw ex;
            }
            catch(Exception ex)
            {
                throw new Exception(Messages.Generic_Error);
            }       
        }

        #endregion Public Methods

        #region Abstract Methods

        protected virtual void Update(TEntity entity, TEntity newValues)
        {
            throw new NotImplementedException();
        }

        protected virtual bool IsValid(TViewModel viewModel)
        {
            return true;
        }


        #endregion Abstract Methods
    }
}