using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;

namespace NHEntity.Entities
{
    [Serializable]
    public class Sections {
        #region Sections
        public virtual Int32 ID {get; set;} 

        public virtual String CName {get; set;} 
        public virtual String EName {get; set;} 
        public virtual String FullPath {get; set;} 
        public virtual String Content {get; set;} 
        public virtual Int32 SectionTempID {get; set;} 
        public virtual Int32 ArticleTempID {get; set;} 
        public virtual Int32 ParentID {get; set;} 
        public virtual Int32 SortNo {get; set;} 
        public virtual String MainPic {get; set;} 
        public virtual Int32 DisplayState {get; set;} 
        public virtual DateTime AddTime {get; set;} 
        public virtual String Memo {get; set;} 
        public virtual Int32 Type {get; set;} 
        public virtual Int32 IssueState {get; set;} 
        public virtual String Description {get; set;} 
        public virtual String Keywords {get; set;} 
        public virtual String SeoTitle {get; set;} 
        public virtual String SeoDescription {get; set;} 
        public virtual String SeoKeywords {get; set;} 
        public virtual String DataModel {get; set;} 
        public virtual String DataPrototype {get; set;} 
        public virtual String DataLabel {get; set;} 
        public virtual String ParentPath {get; set;} 
        public virtual String PageName {get; set;} 
        public virtual Int32 ReferSectionID {get; set;} 
        public virtual Int32 ReferState {get; set;} 
        public virtual String Remarks {get; set;} 
        public virtual String RedirectUrl {get; set;} 

        #endregion
    }
}