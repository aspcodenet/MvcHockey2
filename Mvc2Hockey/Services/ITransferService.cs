using System.Linq;
using Mvc2Hockey.Models;

namespace Mvc2Hockey.Services
{
    public enum TransferErrorCode
    {
        Ok,
        SameTeamError,
        ToTeamMax20,
        FromTeamMin15,ToTeamALreadyTooMeanyPlayersOnThatPosition,
        NoSuchPlayer
    }
    public interface ITransferService
    {
        TransferErrorCode Transfer(int playerId, int newTeamId);

        void Draft(int playerId, int newTeamId);
    }

    public class TransferService : ITransferService
    {
        private readonly HockeyDbContext _context;

        public TransferService(HockeyDbContext context)
        {
            _context = context;
        }



        public TransferErrorCode Transfer(int playerId, int newTeamId)
        {
            var currentPlayer = GetPlayer(playerId);
            if (currentPlayer == null) return TransferErrorCode.NoSuchPlayer;

            if (currentPlayer.Team.Id == newTeamId) return TransferErrorCode.SameTeamError;
            //kontroll,

            //currentPlayer.Team =
            //    _context.SaveChanges();

            return TransferErrorCode.Ok;
        }

        private Player GetPlayer(int playerId)
        {
            var currentPlayer = _context.Players.FirstOrDefault(p => p.Id == playerId);
            return currentPlayer;
        }

        public void Draft(int playerId, int newTeamId)
        {
            throw new System.NotImplementedException();
        }
    }
}