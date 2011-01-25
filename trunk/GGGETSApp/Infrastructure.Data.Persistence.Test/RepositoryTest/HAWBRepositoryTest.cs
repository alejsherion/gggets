using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ETS.GGGETSApp.Domain.Core.Entities;
using System.Linq.Expressions;
using ETS.GGGETSApp.Infrastructure.Data.Persistence.UnitOfWork;
using ETS.GGGETSApp.Infrastructure.CrossCutting.IoC;
using ETS.GGGETSApp.Infrastructure.Data.Core;
using ETS.GGGETSApp.Infrastructure.Data.Core.Extensions;
using ETS.GGGETSApp.Domain.Core;
using System.Reflection;
using ETS.GGGETSApp.Infrastructure.CrossCutting.Logging;
using ETS.GGGETSApp.Domain.Core.Specification;
using ETS.GGGETSApp.Domain.Application.Entities;
using ETS.GGGETSApp.Infrastructure.Data.Persistence.Repositories;
using System.Data.Objects;

namespace Infrastructure.Data.Persistence.Test.RepositoryTest
{
    /// <summary>
    /// Summary description for HAWBRepositoryTest
    /// </summary>
    [TestClass]
    public class HAWBRepositoryTest : RepositoryTestsBase<HAWB>
    {
        [TestMethod()]
        public void Test()
        {
            //Arrange
            IGGGETSAppUnitOfWork context = this.GetUnitOfWork();
            
            
            ObjectContext obContext = (ObjectContext)context;
            ITraceManager traceManager = this.GetTraceManager();
            HAWBRepository repository = new HAWBRepository(context, traceManager);

            HAWB hb = repository.FindHAWBByBarCode("2222");

            Item item = hb.Item.First();
            item.Height = 1000;

            repository.Modify(hb);

            context.CommitAndRefreshChanges();
            //act
            IEnumerable<HAWB> result = repository.GetAll();

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count() > 0);

        }



        public override Expression<Func<HAWB, bool>> FilterExpression
        {
            get { return it => it.Piece!=0;  }
        }

        public override Expression<Func<HAWB, int>> OrderByExpression
        {
            get { return it => (int)it.Piece; }
        }
    }
}
