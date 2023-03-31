using System;
using ConsoleApp1.Entities;

namespace ConsoleApp1.Services {
    class ContractService {

        private IOnlinePaymentService _onlinePaymentService;

        public ContractService(IOnlinePaymentService onlinePaymentService) {
            _onlinePaymentService = onlinePaymentService;
        }

        public void ProcessContract(Contract contract, int months) {

            double installment = contract.TotalValue / months;

            for (int i = 1; i <= months; i++) {

                double quota = installment + _onlinePaymentService.Interest(installment, i);
                quota += _onlinePaymentService.PaymentFee(quota);

                DateTime date = new DateTime(contract.Date.Year, contract.Date.Month + i, contract.Date.Day);

                contract.Installments.Add(new Installment(date, quota));
            }
        }
    }
}
