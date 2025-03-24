using AutoChess.Contracts.Models;

namespace AutoChess.Contracts.DTOs
{
    public class RefreshShopDTO : BaseDTO
    {
        public int NewMoneyBalance { get; set; }
        public Unit[] ShopUnits { get; set; } = [];
    }
}
