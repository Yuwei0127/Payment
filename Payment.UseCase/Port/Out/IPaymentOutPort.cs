using Payment.Entities;
using Payment.SeedWork.Enum;

namespace Payment.UseCase.Port.Out;

public interface IPaymentOutPort
{
    /// <summary>
    /// 取得
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    Task<Entities.Payment> GetAsync(Guid orderId);
    
    /// <summary>
    /// 儲存
    /// </summary>
    /// <param name="topic"></param>
    /// <returns></returns>
    Task<bool> SaveAsync(Entities.Payment topic);

    /// <summary>
    /// 變更付款狀態
    /// </summary>
    /// <param name="orderId"></param>
    /// <param name="paymentStatus"></param>
    /// <returns></returns>
    Task<bool> ChangePaymentStatusAsync(Guid orderId,PaymentStatusEnum paymentStatus);

    /// <summary>
    /// 更新
    /// </summary>
    /// <param name="payment"></param>
    /// <returns></returns>
    Task<bool> UpdateAsync(Entities.Payment payment);
}