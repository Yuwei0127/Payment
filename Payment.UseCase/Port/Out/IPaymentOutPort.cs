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
    Task<Entities.Payment?> GetAsync(Guid orderId);
    
    /// <summary>
    /// 儲存
    /// </summary>
    /// <param name="payment"></param>
    /// <returns></returns>
    Task<bool> SaveAsync(Entities.Payment payment);

    /// <summary>
    /// 更新
    /// </summary>
    /// <param name="payment"></param>
    /// <returns></returns>
    Task<bool> UpdateAsync(Entities.Payment payment);
}