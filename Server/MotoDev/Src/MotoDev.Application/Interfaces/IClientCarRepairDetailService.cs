namespace MotoDev.Application.Interfaces
{
    public interface IClientCarRepairDetailService
    {
        int GetCurrentYearRepairsForOwnerUserId(int ownerUserId);
    }
}