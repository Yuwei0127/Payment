using Microsoft.EntityFrameworkCore;
using Payment.Entities;
using Payment.SeedWork.Enum;
using Payment.UseCase.Port.Out;

namespace Payment.Adapter.Out.Implements;

public class PaymentRepository : IPaymentOutPort
{
    private readonly PaymentDbContext _context;

    public PaymentRepository(PaymentDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// 取得
    /// </summary>
    /// <param name="paymentId"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<Entities.Payment?> GetAsync(Guid paymentId)
    {
        var payment = await _context.Payments
            .Where(p => p.Id == new PaymentId(paymentId))
            .FirstOrDefaultAsync();

        return payment ?? null;
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