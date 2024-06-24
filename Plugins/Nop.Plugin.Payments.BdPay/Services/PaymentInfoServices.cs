﻿using Nop.Data;
using Nop.Plugin.Payments.BdPay.Domain;

namespace Nop.Plugin.Payments.BdPay.Services
{
    public class PaymentInfoServices : IPaymentInfoServices
    {
        private readonly IRepository<PaymentInfo> _paymentRepository;

        public PaymentInfoServices(IRepository<PaymentInfo> paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public virtual async Task InsertPaymentInfoAsync(PaymentInfo paymentInfo)
        {
            await _paymentRepository.InsertAsync(paymentInfo);
        }
    }
}
