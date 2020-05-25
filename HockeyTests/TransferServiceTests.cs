using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mvc2Hockey.Models;
using Mvc2Hockey.Services;
using AutoFixture;

namespace HockeyTests
{
    [TestClass]
    public class TransferServiceTests : BaseTest
    {
        private TransferService sut;



        [TestMethod]
        public void TransferShouldReturnErrorWhenPlayerNotExists()
        {
            var options = new DbContextOptionsBuilder<HockeyDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options;

            var ctx = new HockeyDbContext(options);
            sut = new TransferService(ctx);

            Assert.AreEqual(TransferErrorCode.NoSuchPlayer, sut.Transfer(12, 5));
        }


        [TestMethod]
        public void TransferShouldReturnErrorWhenSameTeams()
        {
            var options = new DbContextOptionsBuilder<HockeyDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options;

            var ctx = new HockeyDbContext(options);
            sut = new TransferService(ctx);
            //var team = fixture.Create<Team>();
            var player = fixture.Create<Player>();
            ctx.Players.Add(player);
            ctx.SaveChanges();

            Assert.AreEqual(TransferErrorCode.SameTeamError, sut.Transfer(player.Id, player.Team.Id));
        }
    }
}
