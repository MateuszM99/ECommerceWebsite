using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceModels.Enums
{
    public enum OrderStatus
    {
        AwaitingPayment,
        ProcessingInProgress,
        PaymentAccepted,
        Shipped,
        Refunded,
        Canceled,
        Delivered,
        OnBackorder
    }
}
