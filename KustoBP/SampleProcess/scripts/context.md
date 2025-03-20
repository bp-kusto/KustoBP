# [ДЕМО ПРОЦЕС](../readme.md) / ⚙️ СЦЕНАРІЇ

```cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using EleWise.ELMA.API;
using EleWise.ELMA.ConfigurationModel;
using EleWise.ELMA.Model.Entities;
using EleWise.ELMA.Model.Managers;
using EleWise.ELMA.Security.Models;

using Kusto.Docflow.Directory;
using Kusto.Docflow.DocGen;
using Kusto.Ext.CFR__Ext;
using Kusto.Ext.Department__Ext;
using Kusto.Ext.Elma_Entity;
using Kusto.Ext.Numerator;
using Kusto.Ext.Obj25045p44_ProcMember;
using Kusto.Ext.Sys_DateTime;
using Kusto.Ext.Sys_Double;
using Kusto.Ext.Sys_Int;
using Kusto.Ext.Sys_TimeSpan;
using Kusto.System.Url;

using Config = EleWise.ELMA.ConfigurationModel.PC25076o02_SampleProcess;
using ConfigBL10_Mbrs = EleWise.ELMA.ConfigurationModel.PC25076o02_BL10_ProcMbr;
using Context = EleWise.ELMA.Model.Entities.ProcessContext.P25076j_SampleProcess;
using ContextBL10_Mbrs = EleWise.ELMA.Model.Entities.ProcessContext.P25076j_BL10_ProcMember;
using Doc = EleWise.ELMA.Documents.Models.D25076o19_SampleDoc;
using DocBL10_Mbr = EleWise.ELMA.Documents.Models.D25076o19_BL10_ProcMember;
using ElmaUsr = EleWise.ELMA.Security.Models.User;
using EProcStatus = EleWise.ELMA.ConfigurationModel.E25076j05_ProcStatus;

namespace EleWise.ELMA.Model.Scripts {
    /// <remarks> 2025-03-19 17-20 • P25076j__SampleProcess
    /// • Group of users
    /// 	[ ] ⓿⚙️ Процеси / 25076j ⚙️ SampleProcess / ▶️ Ініціатори
	/// 	[ ]
    /// 
    /// • Objects
    /// 	[+] ⓿ PC25076o02 🛠️ SampleProcess(Объект) 
    /// 	[+] ⓿❶ 25048m12 📘 TaskId
    /// 	[+] ⓿❶ 25045p44 📘 Учасник процесу
	/// 	[ ]
    /// 
    /// • Enums
    /// 	[ ] ⓿ E25076j05 🎲 Статус процесу(Перечисление)
	/// 	[ ]
    /// 
    /// • Documents
    /// 	[ ] ⓿ D25076o19 📜 SampleDoc(Документ)
	/// 	[ ]
    /// 
    /// • Modules
    ///     [ ] Kusto.Docflow.Directory
    ///     [ ] Kusto.Docflow.DocGen
    ///     [ ] Kusto.Ext.CFR
    ///     [ ] Kusto.Ext.Department
    ///     [ ] Kusto.Ext.Elma_Entity
    ///     [ ] Kusto.Ext.Numerator
    ///     [ ] Kusto.Ext.Obj25045p44_ProcMember
    /// 	[ ] Kusto.Ext.Sys_DateTime
    ///		[ ] Kusto.Ext.Sys_Double
    ///		[ ] Kusto.Ext.Sys_Int
    ///		[ ] Kusto.Ext.Sys_TimeSpan
    ///		[ ] Kusto.Ext.System.Url
	///		[ ]
    ///     
    /// • Aux processes
    ///		[ ]
    ///		
    /// ■ TODO
    ///		[ ] Зкопіювати файл "DT24306n16_ProtocolOfBudgetChanges.xlsx" в папку "C:\KustoDocTemplates\"
    ///     [ ]
    ///		
    /// </remarks>
    public partial class P25076j_SampleProcess_Scripts: EleWise.ELMA.Workflow.Scripts.ProcessScriptBase<Context> {
        // ▬▬▬▬▬ Proc ▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬
        public virtual void Proc_AS(Context context) {
            context.FilterOff(c => c.OurCompany);
            context.FilterOff(c => c.CFR);
            context.FilterOff(c => c.Department);
            //
            context.SetUpProcConfig();
            context.SetUpProcNumber();
            context.UpdateProcDate();
            context.SetUpProcDirectory();
            context.UpdateProcDate();
        }

        public virtual void Proc_BS(Context context) {
        }

        public virtual void Proc_BeforeAbort(Context context) {
            context.UpdateProcNameStatusAndDoc(EProcStatus.Canceled, null);
        }

        // ▬▬▬▬▬ T0000 ▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬
        public virtual void T0000_OnLoad(Context context, EleWise.ELMA.Model.Views.FormViewBuilder<Context> form) {
            try {
                context.SetUpProcConfig();
                context.FilterOn(c => c.OurCompany, Constants.Filters.SuitableOurCompanies);
                CFR__ExtFilter.FilterOn_ActualObjects(context, c => c.CFR);
            }
            catch(Exception ex) {
                form.Notifier.Error(String.Format("{0} • {1}", ex.Message, ex.StackTrace));
            }
        }

        public virtual void T0000_OnCh__CFR(Context context, EleWise.ELMA.Model.Views.FormViewBuilder<Context> form) {
            context.Department = Department__ExtFilter.FilterOn_CfrRelatedActualObjects(context, c => c.Department, c => c.CFR);
        }

        // ▬▬▬▬▬ T1010 ▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬
        public virtual void T1010_B10(Context context) {
            context.UpdateProcNameStatusAndDoc(EProcStatus.Draft, "1010");
        }

        public virtual void T1010_OnLoad(Context context, EleWise.ELMA.Model.Views.FormViewBuilder<Context> form) {
            try {
                context.UpdateTaskExecutor_T2010();
                context.UpdateTaskExecutor_T3010();
            }
            catch(Exception ex) {
                form.Notifier.Error(String.Format("{0} ■ {1}", ex.Message, ex.StackTrace));
            }
        }

        public virtual void T1010_OnCh__ProcSubject(Context context, EleWise.ELMA.Model.Views.FormViewBuilder<Context> form) {
            DateTime currentTimeStamp = DateTime.Now;
            context.DebugStr = String.Format("{0:yyyy.MM.dd(ddd) HH:mm:ss} • Змінено \"Предмет процесу\"", currentTimeStamp);
        }

        public virtual void T1010_A00__Break(Context context) {
            context.SaveExecutorRemarks(context.Z10Usr_Initiator, EProcStatus.Canceled);
            context.UpdateProcNameStatusAndDoc(EProcStatus.Canceled, null);
        }

        public virtual void T1010_A01__Abort(Context context) {
            context.UpdateProcNameStatusAndDoc(EProcStatus.Revoked, null);
        }

        public virtual void T1010_A10__Save(Context context) {
            context.UpdateProcNameStatusAndDoc(EProcStatus.Draft, "1010");
        }

        public virtual void T1010_A20__Ok(Context context) {
            context.SaveExecutorRemarks(context.Z10Usr_Initiator, EProcStatus.UnderReviewBySpecialist);
        }

        // ▬▬▬▬▬ T1010_20 ▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬
        public virtual void T1010_20_OnLoad(Context context, EleWise.ELMA.Model.Views.FormViewBuilder<Context> form) {
        }

        // ▬▬▬▬▬ T1020 ▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬
        public virtual void T1020_B10(Context context) {
            context.UpdateProcNameStatusAndDoc(EProcStatus.UnderCorrection, "1020");
        }

        public virtual void T1020_OnLoad(Context context, EleWise.ELMA.Model.Views.FormViewBuilder<Context> form) {
            try {
                // При завантаженні форми
            }
            catch(Exception ex) {
                form.Notifier.Error(String.Format("{0} ■ {1}", ex.Message, ex.StackTrace));
            }
        }

        public virtual void T1020_A00__Break(Context context) {
            context.SaveExecutorRemarks(context.Z10Usr_Initiator, EProcStatus.Canceled);
            context.UpdateProcNameStatusAndDoc(EProcStatus.Canceled, null);
        }

        public virtual void T1020_A10__Save(Context context) {
        }

        public virtual void T1020_A20__Ok(Context context) {
            context.SaveExecutorRemarks(context.Z10Usr_Initiator, EProcStatus.UnderReviewBySpecialist);
            context.UpdateProcNameStatusAndDoc(EProcStatus.Draft, null);
        }

        // ▬▬▬▬▬ T2010 ▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬
        public virtual void T2010_B10(Context context) {
            context.UpdateProcNameStatusAndDoc(EProcStatus.UnderReviewBySpecialist, "2010");
        }

        public virtual void T2010_OnLoad(Context context, EleWise.ELMA.Model.Views.FormViewBuilder<Context> form) {
            try {
                // При завантаженні форми
            }
            catch(Exception ex) {
                form.Notifier.Error(String.Format("{0} ■ {1}", ex.Message, ex.StackTrace));
            }
        }

        public virtual void T2010_A10__Return(Context context) {
            context.SaveExecutorRemarks(context.Z20Usr_Economist, EProcStatus.UnderCorrection);
            context.UpdateProcNameStatusAndDoc(EProcStatus.UnderCorrection, null);
        }

        public virtual void T2010_A20__Ok(Context context) {
            context.SaveExecutorRemarks(context.Z20Usr_Economist, EProcStatus.UnderReviewByChief);
            context.UpdateProcNameStatusAndDoc(EProcStatus.UnderReviewByChief, null);
        }

        // ▬▬▬▬▬ T3010 ▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬
        public virtual void T3010_B10(Context context) {
            context.UpdateProcNameStatusAndDoc(EProcStatus.UnderReviewByChief, "3010");
        }

        public virtual void T3010_OnLoad(Context context, EleWise.ELMA.Model.Views.FormViewBuilder<Context> form) {
            try {
                // При завантаженні форми
            }
            catch(Exception ex) {
                form.Notifier.Error(String.Format("{0} ■ {1}", ex.Message, ex.StackTrace));
            }
        }

        public virtual void T3010_A10__No(Context context) {
            context.SaveExecutorRemarks(context.Z30Usr_Chief, EProcStatus.Refused);
            context.UpdateProcNameStatusAndDoc(EProcStatus.Refused, null);
        }

        public virtual void T3010_A20__Ok(Context context) {
            context.SaveExecutorRemarks(context.Z30Usr_Chief, EProcStatus.Confirmed);
            context.UpdateProcNameStatusAndDoc(EProcStatus.Confirmed, null);
        }
    }
    //
    public static class Constants {
        public static readonly Guid ProcGuid = new Guid("0195a4a1-f11a-7189-bc83-04892ad669f6");

        public static class ProcTaskGuid {
            public static readonly Guid T2010 = new Guid("f3c6c7c8-2b9b-42bc-b42b-2a484d2e7fb0");

            public static readonly Guid T3010 = new Guid("2c06f6c0-3511-4f64-91c9-4554f48c379d ");
        }

        public static class Abbreviation {
            public const string Process = "ДЕМО";
        }

        public static class Filters {
            public const string SuitableOurCompanies = "Actuality = 'Actual' and Id > 0";
        }
    }
    //
    public static class Context__Ext {
        public static void SetUpProcConfig(this Context context) {
            if(context.ProcConfig != null)
                return;
            //
            Config pc = EntityManager<Config>.Instance.Find(e => e.PC_Actuality == EActuality.Actual && e.PC_UUID == Constants.ProcGuid).FirstOrDefault();
            if(pc == null)
                throw new InvalidOperationException("Proc config not found!");
            context.ProcConfig = pc;
        }

        public static void FilterOn<TEntity>(this Context context, Expression<Func<Context, TEntity>> propertySelector, String filterQuery) where TEntity : IEntity {
            Elma_Entity__ExtFilter.FilterOn(context, propertySelector, filterQuery);
        }

        public static void FilterOff<TEntity>(this Context context, Expression<Func<Context, TEntity>> propertySelector) where TEntity : IEntity {
            Elma_Entity__ExtFilter.FilterOn(context, propertySelector, String.Empty);
        }

        public static void SetUpProcNumber(this Context context) {
            if(context.ProcNumber < 1) {
                context.ProcNumber = Ext__Numerator.GetNextStdNumber(Constants.ProcGuid);
                context.ProcNumberStr = context.ProcNumber.AsStr_ProcNumber();
            }
        }

        public static DateTime UpdateProcDate(this Context context) {
            context.ProcDate = DateTime.Now;
            return context.ProcDate;
        }

        public static void SetUpProcDirectory(this Context context) {
            string ourCompany = context.OurCompany.InternalCode;
            string dirName = String.Format("{0} #{1} {2} -- {3}", Constants.Abbreviation.Process, context.ProcNumberStr, ourCompany, context.ProcSubject);
            if(context.ProcWorkDir != null) {
                context.ProcWorkDir.Name = dirName;
                return;
            }
            //
            context.ProcWorkDir = DirExt.Dir("ДЕМО").Dir(String.Format("{0:yyyy} р.", context.ProcDate)).Dir(String.Format("{0:MM} ({0:MMM})", context.ProcDate)).Dir(ourCompany).Dir(dirName);
        }

        public static void SaveExecutorRemarks(this Context context, ElmaUsr executor, EProcStatus? procStatus) {
            ContextBL10_Mbrs mbr = EntityManager<ContextBL10_Mbrs>.Create();
            context.BL10_ProcMembers.Add(mbr);
            //
            mbr.BI_AttachmentsWithDocs.Clear();
            mbr.BI_AttachmentsWithDocs.AddAll(context.AttachmentsWithDocs);
            context.AttachmentsWithDocs.Clear();
            mbr.BI_AttachmentsWithFiles.Clear();
            mbr.BI_AttachmentsWithFiles.AddAll(context.AttachmentsWithFiles);
            context.AttachmentsWithFiles.Clear();
            mbr.BI_Executor = executor;
            mbr.BI_OrderNumb = context.BL10_ProcMembers.Count();
            mbr.BI_ProcStatus = procStatus ?? context.ProcStatus;
            mbr.BI_Rem = context.Remarks;
            context.Remarks = String.Empty;
            mbr.BI_TimeStamp = DateTime.Now;
        }

        public static void UpdateProcNameStatusAndDoc(this Context context, EProcStatus? newStatus, String taskCode) {
            EProcStatus status = context.ProcStatus = newStatus ?? context.ProcStatus;
            String ourCompanyCode = context.OurCompany == null ? "" : context.OurCompany.InternalCode;
            context.ProcId = String.Format("{0} {1}{2} {3}", Constants.Abbreviation.Process, context.ProcNumberStr, String.IsNullOrWhiteSpace(taskCode) ? "" : "~" + taskCode, context.ProcStatus.Sign());
            context.ProcName = String.Format("{0} #{1}{2} {3} -- {4}"
                , Constants.Abbreviation.Process
                , context.ProcNumberStr
                , context.ProcStatus.Sign()
                , ourCompanyCode
                , context.ProcSubject);
            context.ProcDoc.Update(context);
        }

        public static ProcessStartInfo GetProcessStartInfo(this Context context) {
            return new ProcessStartInfo(context.Z10Usr_Initiator, context.OurCompany, context.CFR, context.Department);
        }

        private static ElmaUsr GetTaskExecutor(this Context context, Guid taskGuid) {
            ConfigBL10_Mbrs executorFromConfig = context.ProcConfig.GetTaskExecutor(taskGuid);
            return executorFromConfig == null ? null : executorFromConfig.BI_Mbr.GetExecutor(context.GetProcessStartInfo());
        }

        public static void UpdateTaskExecutor_T2010(this Context context) {
            context.Z20Usr_Economist = context.GetTaskExecutor(Constants.ProcTaskGuid.T2010);
        }

        public static void UpdateTaskExecutor_T3010(this Context context) {
            context.Z30Usr_Chief = context.GetTaskExecutor(Constants.ProcTaskGuid.T3010);
        }
    }
    //
    public static class Config__Ext {
        public static ConfigBL10_Mbrs GetTaskExecutor(this Config config, Guid taskGuid) {
            return config.BL10_ProcMbrs.Where(e => e.BI_Mbr != null && e.BI_Mbr.Actuality == EActuality.Actual && e.BI_Mbr.TaskId != null && e.BI_Mbr.TaskId.Actuality == EActuality.Actual && e.BI_Mbr.TaskId.ProcUUID == Constants.ProcGuid && e.BI_Mbr.TaskId.TaskUUID == taskGuid).FirstOrDefault();
        }
    }
    //
    public class ProcessStartInfo: IProcessStartInfo {
        public ElmaUsr ProcInitiator { get; set; }
        public OurCompany OurCompany { get; set; }
        public CFR CFR { get; set; }
        public Department Department { get; set; }

        public ProcessStartInfo(ElmaUsr procInitiator, OurCompany ourCompany, CFR cfr, Department department) {
            ProcInitiator = procInitiator;
            OurCompany = ourCompany;
            CFR = cfr;
            Department = department;
        }
    }
    //
    public static class Entity__Ext {
        public static String AsStr<TE, TP>(this TE entity, Func<TE, TP> propSelector, String formatString = default(String), String emptyResult = "❓") {
            if(entity == null)
                return emptyResult;
            TP propValue = propSelector(entity);
            if(propValue == null)
                return emptyResult;
            return String.IsNullOrWhiteSpace(formatString) ? propValue.ToString() : String.Format(formatString, propValue);
        }
    }
    //
    public static class EProcStatus__Ext {
        public static String Sign(this EProcStatus? value) {
            return value == null ? String.Empty : ((EProcStatus)value).Sign();
        }

        public static String Sign(this EProcStatus value) {
            return E__Sign[value];
        }

        public static String SignStr(this EProcStatus? value) {
            return value == null ? String.Empty : ((EProcStatus)value).SignStr();
        }

        public static String SignStr(this EProcStatus value) {
            String sign = value.Sign();
            sign = String.IsNullOrWhiteSpace(sign) ? String.Empty : sign + " ";
            return sign + value.Str();
        }

        public static String Str(this EProcStatus? value) {
            return value == null ? String.Empty : ((EProcStatus)value).Str();
        }

        public static String Str(this EProcStatus value) {
            return E__Str[value];
        }

        static EProcStatus__Ext() {
            Register("📝", EProcStatus.Draft, "Чернетка");
            Register("⚠️", EProcStatus.UnderCorrection, "На доопрацюванні");
            Register("🗑️", EProcStatus.Canceled, "Ануловано");
            Register("⛔", EProcStatus.Revoked, "Застаріло");
            Register("🔎", EProcStatus.UnderReviewBySpecialist, "На розгляді спеціаліста");
            Register("⏳", EProcStatus.UnderReviewByChief, "На розгляді керівника");
            Register("❌", EProcStatus.Refused, "Відхилено");
            Register("✔️", EProcStatus.Confirmed, "Схвалено");
        }

        private static void Register(String sign, EProcStatus val, String str) {
            E__Sign.Add(val, sign);
            E__Str.Add(val, str);
        }

        private static readonly Dictionary<EProcStatus, String> E__Sign = new Dictionary<EProcStatus, String>();

        private static readonly Dictionary<EProcStatus, String> E__Str = new Dictionary<EProcStatus, String>();
    }
    //
    public static class ProcDoc__Ext {
        public static Doc Update(this Doc procDoc, Context context) {
            Context c = context;

            Doc doc = procDoc ?? PublicAPI.Docflow.Types.UserD25076o19_SampleDoc
                .Create(DocGen__Ext.CreateEmptyBinaryFile(Constants.Abbreviation.Process), c.ProcWorkDir, c.GetDocTitle());
            //
            doc.Usr_Initiator = c.Z10Usr_Initiator;
            doc.DocNumber = c.ProcNumber;
            doc.DocNumberStr = c.ProcNumberStr;
            doc.DocDate = c.ProcDate;
            doc.DocStatus = c.ProcStatus;
            doc.OurCompany = c.OurCompany;
            doc.CFR = c.CFR;
            doc.Departament = c.Department;
            doc.DocSubject = c.ProcSubject;
            doc.DocDescription = c.Description;
            //
            doc.BL10_ProcMembers.ToList().ForEach(e => doc.BL10_ProcMembers.Remove(e));
            foreach(ContextBL10_Mbrs mbr in context.BL10_ProcMembers) {
                DocBL10_Mbr docMbr = EntityManager<DocBL10_Mbr>.Create();
                doc.BL10_ProcMembers.Add(docMbr);
                //
                docMbr.BI_OrderNumb = mbr.BI_OrderNumb;
                docMbr.BI_ProcStatus = mbr.BI_ProcStatus;
                docMbr.BI_TimeStamp = mbr.BI_TimeStamp;
                docMbr.BI_Executor = mbr.BI_Executor;
                docMbr.BI_Rem = mbr.BI_Rem;
                docMbr.BI_AttachmentsWithFiles.Clear();
                docMbr.BI_AttachmentsWithFiles.AddAll(mbr.BI_AttachmentsWithFiles);
                docMbr.BI_AttachmentsWithDocs.Clear();
                docMbr.BI_AttachmentsWithDocs.AddAll(mbr.BI_AttachmentsWithDocs);
            }
            //
            doc.Name = context.GetDocTitle();
            doc.CurrentVersion.File = DocGen__Ext.GenerateFromTemplate(new StrViewProcDoc(doc, context), c.ProcConfig.FilePathToProcDocTemplate, String.Format("{0} {1}", Constants.Abbreviation.Process, context.ProcNumberStr));
            //
            context.ProcDoc = doc;
            return doc;
        }

        public static String GetDocTitle(this Context context) {
            String ourCompanyCode = context.OurCompany == null ? "" : context.OurCompany.InternalCode;

            return String.Format("{0} #{1}{2} {3} -- {4}"
                , Constants.Abbreviation.Process
                , context.ProcNumberStr
                , context.ProcStatus.Sign()
                , ourCompanyCode
                , context.ProcSubject);
        }
    }

    public class StrViewProcDoc {
        public string ElmaDocRef { get; set; }
        public string DocTitle { get; set; }
        public string Usr_Initiator { get; set; }
        public string DocNumberStr { get; set; }
        public string DocDate { get; set; }
        public string DocStatus { get; set; }
        public string OurCompany { get; set; }
        public string CFR { get; set; }
        public string Department { get; set; }
        public string DocSubject { get; set; }
        public string DocDescription { get; set; }
        public List<StrViewProcMbr> ProcMembers { get; set; }
        //
        public StrViewProcDoc(Doc doc, Context context) {
            ElmaDocRef = Url__Ext.GetUrl(doc);
            DocTitle = String.Format("{0} #{1}{2} {3}"
                , Constants.Abbreviation.Process
                , doc.DocNumberStr
                , doc.DocStatus.Sign()
                , doc.OurCompany.AsStr(e => e.InternalCode, null, "--"));
            Usr_Initiator = doc.Usr_Initiator.AsStr(e => e.GetShortNamePosition(), null, "--");
            DocNumberStr = doc.DocNumberStr;
            DocDate = doc.DocDate.AsStr();
            DocStatus = doc.DocStatus.SignStr();
            OurCompany = doc.OurCompany.AsStr(e => e.Name, null, "--");
            CFR = doc.CFR.AsStr(e => e.Name, null, "--");
            Department = doc.Departament.AsStr(e => e.Name, null, "--");
            DocSubject = doc.DocSubject;
            DocDescription = doc.DocDescription;
            //
            ProcMembers = doc.BL10_ProcMembers.OrderByDescending(e => e.BI_OrderNumb).Select(e => new StrViewProcMbr(e)).ToList();
        }
    }

    public class StrViewProcMbr {
        public string OrderNumber { get; set; }
        public string ProcStatus { get; set; }
        public string TimeStamp { get; set; }
        public string Executor { get; set; }
        public string Remarks { get; set; }
        public string Attachments { get; set; }
        //
        public StrViewProcMbr(DocBL10_Mbr mbr) {
            OrderNumber = mbr.BI_OrderNumb.AsStr();
            ProcStatus = mbr.BI_ProcStatus.SignStr();
            TimeStamp = mbr.BI_TimeStamp.AsStr();
            Executor = mbr.BI_Executor.AsStr(e => e.GetShortNamePosition(), null, "--");
            Remarks = mbr.BI_Rem;
            Attachments = String.Format("Файлів: {0} \nДокументів: {1}"
                , mbr.BI_AttachmentsWithDocs.Count.AsStrMU("шт.")
                , mbr.BI_AttachmentsWithDocs.Count.AsStrMU("шт."));
        }
    }
}

```