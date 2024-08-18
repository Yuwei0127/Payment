using Payment.SeedWork.Enum;
using Payment.UseCase.Port.Out;

namespace Payment.Adapter.Out.Implements;

public class PaymentRepository : IPaymentOutPort
{
    /// <summary>
    /// 取得
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task<Entities.Payment> GetAsync(Guid orderId)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// 儲存
    /// </summary>
    /// <param name="topic"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task<bool> SaveAsync(Entities.Payment topic)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// 變更付款狀態
    /// </summary>
    /// <param name="orderId"></param>
    /// <param name="paymentStatus"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task<bool> ChangePaymentStatusAsync(Guid orderId, PaymentStatusEnum paymentStatus)
    {
        throw new NotImplementedException();
    }
    
    /// <summary>
    /// 更新
    /// </summary>
    /// <param name="payment"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task<bool> UpdateAsync(Entities.Payment payment)
    {
        throw new NotImplementedException();
    }
}