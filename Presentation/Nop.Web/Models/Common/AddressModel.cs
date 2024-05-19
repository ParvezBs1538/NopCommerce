﻿using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Core.Domain.Common;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Web.Models.Common;

public partial record AddressModel : BaseNopEntityModel
{
    public AddressModel()
    {
        AvailableCountries = new List<SelectListItem>();
        AvailableStates = new List<SelectListItem>();
        CustomAddressAttributes = new List<AddressAttributeModel>();
        AddressFields = new KeyValuePair<AddressField, string>[7];
    }

    [NopResourceDisplayName("Address.Fields.FirstName")]
    public string FirstName { get; set; }
    [NopResourceDisplayName("Address.Fields.LastName")]
    public string LastName { get; set; }
    [DataType(DataType.EmailAddress)]
    [NopResourceDisplayName("Address.Fields.Email")]
    public string Email { get; set; }


    public bool CompanyEnabled { get; set; }
    public bool CompanyRequired { get; set; }
    [NopResourceDisplayName("Address.Fields.Company")]
    public string Company { get; set; }

    public bool CountryEnabled { get; set; }
    [NopResourceDisplayName("Address.Fields.Country")]
    public int? CountryId { get; set; }
    [NopResourceDisplayName("Address.Fields.Country")]
    public string CountryName { get; set; }

    public int? DefaultCountryId { get; set; }

    public bool StateProvinceEnabled { get; set; }
    [NopResourceDisplayName("Address.Fields.StateProvince")]
    public int? StateProvinceId { get; set; }
    [NopResourceDisplayName("Address.Fields.StateProvince")]
    public string StateProvinceName { get; set; }

    public bool CountyEnabled { get; set; }
    public bool CountyRequired { get; set; }
    [NopResourceDisplayName("Address.Fields.County")]
    public string County { get; set; }

    public bool CityEnabled { get; set; }
    public bool CityRequired { get; set; }
    [NopResourceDisplayName("Address.Fields.City")]
    public string City { get; set; }

    public bool StreetAddressEnabled { get; set; }
    public bool StreetAddressRequired { get; set; }
    [NopResourceDisplayName("Address.Fields.Address1")]
    public string Address1 { get; set; }

    public bool StreetAddress2Enabled { get; set; }
    public bool StreetAddress2Required { get; set; }
    [NopResourceDisplayName("Address.Fields.Address2")]
    public string Address2 { get; set; }

    public bool ZipPostalCodeEnabled { get; set; }
    public bool ZipPostalCodeRequired { get; set; }
    [NopResourceDisplayName("Address.Fields.ZipPostalCode")]
    public string ZipPostalCode { get; set; }

    public bool PhoneEnabled { get; set; }
    public bool PhoneRequired { get; set; }
    [DataType(DataType.PhoneNumber)]
    [NopResourceDisplayName("Address.Fields.PhoneNumber")]
    public string PhoneNumber { get; set; }

    public bool FaxEnabled { get; set; }
    public bool FaxRequired { get; set; }
    [NopResourceDisplayName("Address.Fields.FaxNumber")]
    public string FaxNumber { get; set; }

    public string AddressLine { get; set; }
    public KeyValuePair<AddressField, string>[] AddressFields { get; set; }

    public IList<SelectListItem> AvailableCountries { get; set; }
    public IList<SelectListItem> AvailableStates { get; set; }

    public string FormattedCustomAddressAttributes { get; set; }
    public IList<AddressAttributeModel> CustomAddressAttributes { get; set; }

    public Address ToEntity(Address destination = null)
    {
        destination ??= new Address();
        
        destination.Id = Id;
        destination.FirstName = FirstName;
        destination.LastName = LastName;
        destination.Email = Email;
        destination.Company = Company;
        destination.CountryId = CountryId == 0 ? null : CountryId;
        destination.StateProvinceId = StateProvinceId == 0 ? null : StateProvinceId;
        destination.County = County;
        destination.City = City;
        destination.Address1 = Address1;
        destination.Address2 = Address2;
        destination.ZipPostalCode = ZipPostalCode;
        destination.PhoneNumber = PhoneNumber;
        destination.FaxNumber = FaxNumber;

        return destination;
    }
}