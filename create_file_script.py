import shutil
import os
file_array =["Abbreviation","Acronym","ActiveConsumerCm","AdsIfspformCorrection","ArL234","As400Output","BabyCder","BabyCderCopy","ButtonLabel","CAllCaseLoadCount","Cder","Cder08","Cder08Comment","Cder08Current","Cder08Label","CderBackup","CderBackupSclicdd","CderBackupSclicde","CderBackupSclicdm","CderBackupSclicdr","CderBackupSclisup","CderLabel","CderModify","CderSclicdm","CderSclicdr","CderSup","Class","Complaint","Consumer","ConsumerAdd","ConsumerAll","ConsumerAll02212020","ConsumerAllBackUp","ConsumerAllDelete","ConsumerCopay","ConsumerDelete","ConsumerHistory","ConsumerInfo","ConsumerLabel","ConsumerLabelcolor","ConsumerLabeltip","ConsumerLoc","ConsumerLocation","ConsumerLocation012919","ConsumerPhoto","ConsumerSdp","ConsumerStat","ConsumerStatsDelete","CrformList","DeletedConsumer","DeletedMessage","DeletedResource","DeletedRtransaction","DeletedSir","DeletedTickler","DeletedTransaction","DispOption","DownloadList","Drug","DrugsLabel","Dsmcodpf","Eligibility","EligibilityEsr","EligibilityRecommendation","EligibilityTurning3","Employment","Error","ErrorViewModel","Esr11","EsrDelay","EventColumn","EventColumns2","EventCommentLabel","EventDefinition","EventTrxComment","EventTrxDatum","EventTrxDef","Fcpp","Financial","Flxdadr","Flxdcde","Flxdcdr","Flxdcli","Flxdcsup","FlxdLog","Flxdrel","Flxdvn","Flxdvnc","Flxdvnd","Form","Form101","Form101Lf","Form101Mc","FormAnnualContact","FormCaseLoadDelegate","FormCssintake","FormDeflectionNotice","FormEsrPsychoSocial","FormEsrTrf","FormFairHearing","FormIfsp","FormIfspDoctor","FormIfspMedication","FormIfspout","FormIfspoutPlan","FormIfspparticipant","FormIfsppr","FormIfspproutcome","FormIntakeSum","FormIpp","FormIppaddendum","FormIppannualFdlrc","FormIppannualFdlrcBak","FormIppfdlrc","FormIppout","FormIppoutPlan","FormIppplanGroup","FormIppplanGroupItem","FormIppplanItem","FormIppplanTitle","FormIppquarterly","FormIrct","FormIrctMapField","FormLawEnforce","FormMissingPerson","FormMt","FormMtanswer","FormMtBk","FormMtcitation","FormMtdayProgram","FormMtdriver","FormMtquestion","FormMtvehicle","FormPermissionAssign","Forms2","FormScdca","FormScmonth","FormScmonthold","FormScmonthSum","FormSocial","FormTransCaseLog","FormTransCaseLogUnit","FormTransition","FormVa","Ftemplate","GeneralInfoClient","GeneralLabel","GeneralRtxtyClient","GeneralTxtyClient","GiServer","Group","GroupPermission","Holiday","Icd10fl","Icd9fl","Icd9flP","IdnotesLabel","Image","Imd","Initialization","Judicial","KeaDevFormIfspBak","LabelSet","Legal","LegalOld","Login","LoginPermission","LoginStat","LoginTrxXrf","LoginType","MedAppointment","Medication","MedInformation","Messaging","Mwc","Mwc03082021","Mwc11052020","MwcDevBackup","OperLog","OperLog042019","OperTrace","PayrollDetail","PayrollPeriod","PayrollSchedule","PayrollTimecard","PayrollUser","Pbcatcol","Pbcatedt","Pbcatfmt","Pbcattbl","Pbcatvld","Pcp","PcpAddedPlan","PcpTable","PcpTableParam","Po","PoliceDept","PosAuth","PosLabel","PosUf","Psppyl","Psrate","Pvdslv","PvndorToResAll","RAllCaseLoadCount","Rc","Report","Resource","ResourceAll","ResourceLabel","RptCaseMgnPlan","RptSetting","Rtemplate","Rtransaction","RtrxType","SandisSclient","SandisSclientUpdate","SandisSpromat","SandisSprotax","SandisSprotaxUpdate","SandisSprotaxUpdate2","SandisSprovid","SandisSprovidUpdate","SchoolDistrict","Scliadr1","Scliadr2","Scliadr3","Sclicde","SclicdeBreakout","SclicdeCder08","Sclicdm","Sclicdr","Sclient","SclientDelete","SclientDob","Sclifcp","Sclirel","SclirelDob","Sclisup","Search","SearchTb","SecuritySet","Securityset04262022","ServiceRequest","SignificantOther","SignificantOtherDelete","SignificantOtherOld","Sir","SirAddendum","SirAddendumTemp","SirMortality","SirReportableCountsLast12","SirTemp","Sl","Sprohst","Sprovid","StatusMessage","Table","Tables04122012","Tables2","Tables2v","TeamMaster","TeamNote","TeamSchedule","Template","Tickler","TicklersSet","TicklersTask","TicklersTb","Todo","ToolRunDate","Transaction","TransactionArchive","TransactionDraft","TransactionHistory","TransactionHistory072318","TransactionsAll","TransactionsAll2","TransactionsDeleted","TransactionsDeleteDevBackup","TransactionsDeleteDevBackupNotHistory","TransferReq","TransferTemp","TrxAddendum","TrxAddendumB","TrxSumByMonth","TrxType","Tsr","User","UserConfig","UserPhone","UsersGroup","UsersSecurity","UsersType","ViewCderAut","ViewCderCp","ViewCderEpilepsy","ViewClientStatus","ViewCountyCode","ViewCurrentResidence","ViewDayProgramSchool","ViewDsmcodpf","ViewEsEligibleCondition","ViewFormsource","ViewIntellFunctLevCderPcp","ViewLang","ViewLegalStatus","ViewLicenseType","ViewMaritalStatus","ViewParentVendorOpenAuth","ViewPosCount","ViewPrimProgName","ViewR","ViewRatio","ViewRelationship","ViewScheduleCurrentService","ViewScheduleDisposition","ViewScheduleDispositionFor","ViewScheduleEsDiagnosis","ViewScheduleEsEligibleCondition","ViewScheduleLevelOfRespite","ViewScheduleReferralVendor1","ViewScheduleReferralVendor2","ViewScheduleReferralVendor3","ViewScheduleReferralVendor4","ViewScheduleReferralVendor5","ViewScheduleReferralVendor6","ViewScheduleServiceCategory","ViewScheduleServiceDesc","ViewScheduleSlIlBudget","ViewScheduleType","ViewScheduleTypePresent","ViewScheduleVisitBy","ViewSchool","ViewServiceCode","ViewSirMederTotal","ViewSpecialist","ViewStatus","ViewTablesG","ViewTablesL","ViewTrxFaceToFace","VoterRegistration","WebLink"]
for i in range(len(file_array)):
    print(file_array[i])
    with open("xxxxxService.cs", "r") as file:
        # Read the contents of the file
        file_contents = file.read()

    # Replace the desired text in the contents
    file_contents = file_contents.replace("Xxxxxx", file_array[i])

    # Create a new file name by combining the keyword and file extension
    new_file_name = file_array[i]+"Service" + ".cs"

    # Open the new file in write mode
    with open(new_file_name, "w") as file:
        # Write the modified contents to the new file
        file.write(file_contents)

    # Source file location
    src_file = file_array[i]+"Service" + ".cs"

    # Destination folder
    dst_folder = "service"

    # Destination file location
    dst_file = os.path.join(dst_folder, file_array[i]+"Service" + ".cs")

    # Move the file
    shutil.move(src_file, dst_file)


