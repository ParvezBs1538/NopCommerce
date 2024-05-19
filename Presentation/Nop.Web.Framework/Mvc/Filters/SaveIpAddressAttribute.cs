﻿using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Nop.Core;
using Nop.Core.Domain.Customers;
using Nop.Core.Http.Extensions;
using Nop.Data;

namespace Nop.Web.Framework.Mvc.Filters;

/// <summary>
/// Represents filter attribute that saves last IP address of customer
/// </summary>
public sealed class SaveIpAddressAttribute : TypeFilterAttribute
{
    #region Ctor

    /// <summary>
    /// Create instance of the filter attribute
    /// </summary>
    public SaveIpAddressAttribute() : base(typeof(SaveIpAddressFilter))
    {
    }

    #endregion

    #region Nested filter

    /// <summary>
    /// Represents a filter that saves last IP address of customer
    /// </summary>
    private class SaveIpAddressFilter : IAsyncActionFilter
    {
        #region Fields

        protected readonly CustomerSettings _customerSettings;
        protected readonly IRepository<Customer> _customerRepository;
        protected readonly IWebHelper _webHelper;
        protected readonly IWorkContext _workContext;

        #endregion

        #region Ctor

        public SaveIpAddressFilter(CustomerSettings customerSettings,
            IRepository<Customer> customerRepository,
            IWebHelper webHelper,
            IWorkContext workContext)
        {
            _customerSettings = customerSettings;
            _customerRepository = customerRepository;
            _webHelper = webHelper;
            _workContext = workContext;
        }

        #endregion

        #region Utilities

        /// <summary>
        /// Called asynchronously before the action, after model binding is complete.
        /// </summary>
        /// <param name="context">A context for action filters</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        private async Task SaveIpAddressAsync(ActionExecutingContext context)
        {
            ArgumentNullException.ThrowIfNull(context);

            //only in GET requests
            if (!context.HttpContext.Request.IsGetRequest())
                return;

            if (!DataSettingsManager.IsDatabaseInstalled())
                return;

            //check whether we store IP addresses
            if (!_customerSettings.StoreIpAddresses)
                return;

            //get current IP address
            var currentIpAddress = _webHelper.GetCurrentIpAddress();

            if (string.IsNullOrEmpty(currentIpAddress))
                return;

            //update customer's IP address
            var customer = await _workContext.GetCurrentCustomerAsync();
            if (_workContext.OriginalCustomerIfImpersonated == null &&
                !currentIpAddress.Equals(customer.LastIpAddress, StringComparison.InvariantCultureIgnoreCase))
            {
                customer.LastIpAddress = currentIpAddress;

                //update customer without event notification
                await _customerRepository.UpdateAsync(customer, false);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Called asynchronously before the action, after model binding is complete.
        /// </summary>
        /// <param name="context">A context for action filters</param>
        /// <param name="next">A delegate invoked to execute the next action filter or the action itself</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            await SaveIpAddressAsync(context);
            if (context.Result == null)
                await next();
        }

        #endregion
    }

    #endregion
}