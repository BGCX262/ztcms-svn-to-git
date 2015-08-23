using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;

namespace NHEntity.Entities
{
    [Serializable]
    public class WebUser {
        #region WebUser
        public virtual Int32 ID {get; set;} 

        public virtual String LoginID {get; set;} 
        public virtual String Password {get; set;} 
        public virtual String Name {get; set;} 
        public virtual String WebsiteList {get; set;} 
        public virtual Int32 StateWebSupperManager {get; set;} 
        public virtual String Permissions {get; set;} 
        public virtual DateTime InTime {get; set;} 
        public virtual DateTime OutTime {get; set;} 
        public virtual String RoleList {get; set;} 
        public virtual Int32 StateEnable {get; set;} 

        #endregion
    }
}