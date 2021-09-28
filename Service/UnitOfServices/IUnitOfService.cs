using Service.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.UnitOfServices
{
    public interface IUnitOfService
    {
        ViewControlService ViewControlService { get; }
        AccessPermissionService AccessPermissionService { get; }
        ActionPermissionService ActionPermissionService { get; }
        UserService UserService { get; }
        RoleService RoleService { get; }
        BedAllocationService BedAllocationService { get; }
        BedService BedService { get; }
        BranchService BranchService { get; }
        BuildingService BuildingService { get; }
        CityService CityService { get; }
        CountryService CountryService { get; }
        FloorService FloorService { get; }
        LookupService LookupService { get; }
        PatientService PatientService { get;}
        PersonalInformationService PersonalInformationService { get; }
        PatientPersonalInformationService PatientPersonalInformationService { get; }
        RoomService RoomService { get; }
        AppointmentService AppointmentService { get; }
        ActionControlService ActionControlService { get; }
        EducationLevelService EducationLevelService { get; }
        LogTableService LogTableService { get; }
        AttachmentService AttachmentService { get; }
        PatientEncounterService PatientEncounterService { get; }
        CompanyProfileService CompanyProfileService { get; }
        EmailService EmailService { get; }
        SMSService SMSService { get; }
        TemplateService TemplateService { get; }
        PatientOrderService PatientOrderService { get; }
        DynamicTemplateService DynamicTemplateService { get; }
        ClientOrderService ClientOrderService { get; }
        MedicalDictionaryService MedicalDictionaryService { get; }
        TestDynamicTemplateService TestDynamicTemplateService { get; }
        TestTempService TestTempService { get; }
        ReminderService ReminderService { get; }
        FollowUpService FollowUpService { get; }
        AppointmentExpService AppointmentExpService { get; }
    }
}

