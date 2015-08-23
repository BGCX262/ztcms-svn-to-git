using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;

namespace NHEntity.Entities
{
    [Serializable]
    public class Article {
        #region Article
        public virtual Int32 ID {get; set;} 

        public virtual String Title {get; set;} 
        public virtual String SubTitle {get; set;} 
        public virtual String Content {get; set;} 
        public virtual String MainPic {get; set;} 
        public virtual String MainMiniPic {get; set;} 
        public virtual String ArticleInMan {get; set;} 
        public virtual String Summary {get; set;} 
        public virtual DateTime InTime {get; set;} 
        public virtual DateTime IssueTime {get; set;} 
        public virtual String KeyWords {get; set;} 
        public virtual String Description {get; set;} 
        public virtual String FilePath {get; set;} 
        public virtual String VedioPath {get; set;} 
        public virtual String SeoTitle {get; set;} 
        public virtual String SeoKeywords {get; set;} 
        public virtual String Seodescription {get; set;} 
        public virtual String Author {get; set;} 
        public virtual String ArticleSource {get; set;} 
        public virtual String RedirectUrl {get; set;} 
        public virtual String Expand {get; set;} 
        public virtual Int32 Recycling {get; set;} 
        public virtual Int32 WSID {get; set;} 
        public virtual String BelongsField {get; set;} 
        public virtual String Stage {get; set;} 
        public virtual String Funds {get; set;} 
        public virtual Int32 HasSample {get; set;} 
        public virtual Int32 Identify {get; set;} 
        public virtual String Transfer {get; set;} 
        public virtual String License {get; set;} 
        public virtual String Cooperation {get; set;} 
        public virtual String OtherTransfer {get; set;} 
        public virtual String Background {get; set;} 
        public virtual String Innovation {get; set;} 
        public virtual String Implement {get; set;} 
        public virtual String Outlook {get; set;} 
        public virtual String Scale {get; set;} 
        public virtual String Benefit {get; set;} 
        public virtual String ApplyField {get; set;} 
        public virtual String NeedFunds {get; set;} 
        public virtual DateTime BeginTime {get; set;} 
        public virtual DateTime EndTime {get; set;} 
        public virtual DateTime PlanEndTime {get; set;} 
        public virtual String EffectiveTime {get; set;} 
        public virtual String Organize {get; set;} 
        public virtual String Tel {get; set;} 
        public virtual String Mobile {get; set;} 
        public virtual String DemandSummary {get; set;} 
        public virtual String Targets {get; set;} 
        public virtual String CurrentBase {get; set;} 
        public virtual String CooperationMode {get; set;} 
        public virtual String AboutField {get; set;} 
        public virtual Int32 Sex {get; set;} 
        public virtual DateTime Berthday {get; set;} 
        public virtual String Professional {get; set;} 
        public virtual String Skill {get; set;} 
        public virtual String PlaceUnit {get; set;} 
        public virtual String JobTitle {get; set;} 
        public virtual String City {get; set;} 
        public virtual String ZipCode {get; set;} 
        public virtual String Fax {get; set;} 
        public virtual String Email {get; set;} 
        public virtual String Industry {get; set;} 
        public virtual String Address {get; set;} 
        public virtual String ContectPeople {get; set;} 
        public virtual String WebUrl {get; set;} 
        public virtual String SelfType {get; set;} 
        public virtual String FinallyCompany {get; set;} 
        public virtual String FinallyPeople {get; set;} 
        public virtual String ContectUnit {get; set;} 
        public virtual String Maturity {get; set;} 

        #endregion
    }
}