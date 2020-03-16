using Microsoft.VisualStudio.TestTools.UnitTesting;
using SuncereDataCenter.Basic.CryptoTransverters;
using SuncereDataCenter.Core.AirQuality;
using SuncereDataCenter.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuncereDataCenter.UnitTest.Model
{
    [TestClass]
    public class ModelTest
    {
        [TestMethod]
        public void Test()
        {
            Random rand = new Random();
            AirQualityCompositeIndexCalculator calculator = new AirQualityCompositeIndexCalculator();
            calculator.CheckIntegrity = true;
            List<CityMonthlyAirQuality> list = new List<CityMonthlyAirQuality>();
            for (int i = 0; i < 100; i++)
            {
                CityMonthlyAirQuality item = new CityMonthlyAirQuality()
                {
                    Code = string.Format("Code{0}", i.ToString().PadLeft(3, '0')),
                    Time = DateTime.Today,
                    Name = string.Format("Name{0}", i.ToString().PadLeft(3, '0')),
                    SO2 = Math.Round(rand.NextDouble() * 60),
                    NO2 = Math.Round(rand.NextDouble() * 40),
                    PM10 = Math.Round(rand.NextDouble() * 70),
                    CO = Math.Round(rand.NextDouble() * 4, 1),
                    O3 = Math.Round(rand.NextDouble() * 160),
                    PM25 = Math.Round(rand.NextDouble() * 35),
                    StandardDays = rand.Next(30)
                };
                //calculator.CalculateAirQualityCompositeIndex(item);
                list.Add(item);
            }
            using (SuncereDataCenterModel db = new SuncereDataCenterModel())
            {
                db.CityMonthlyAirQuality.AddRange(list);
                db.SaveChanges();
            }
        }

        [TestMethod]
        public void Init()
        {
            using (SuncereDataCenterModel db = new SuncereDataCenterModel())
            {
                DateTime now = DateTime.Now;
                SuncereUser user = new SuncereUser()
                {
                    UserName = "admin",
                    DisplayName = "系统管理员",
                    Password = SHA1Encryption.Default.EncryptPassword("Suncere@123"),
                    Status = true,
                    Static = true,
                    CreationTime = now
                };
                SuncereRole role = new SuncereRole()
                {
                    Name = "系统管理员",
                    Status = true,
                    Static = true,
                    CreationTime = now
                };
                user.SuncereRole.Add(role);
                db.SuncereUser.Add(user);
                SuncerePermission systemPermission = new SuncerePermission()
                {
                    Name = "系统管理",
                    Type = 1,
                    Controller = "System",
                    Order = 99,
                    Icon = "system",
                    Status = true,
                    Static = true,
                    CreationTime = now
                };
                SuncerePermission userPermission = new SuncerePermission()
                {
                    Name = "用户管理",
                    Type = 2,
                    Controller = "System",
                    Action = "UserList",
                    Order = 1,
                    ParentId = 1,
                    Status = true,
                    Static = true,
                    CreationTime = now
                };
                SuncerePermission rolePermission = new SuncerePermission()
                {
                    Name = "角色管理",
                    Type = 2,
                    Controller = "System",
                    Action = "RoleList",
                    Order = 2,
                    ParentId = 1,
                    Status = true,
                    Static = true,
                    CreationTime = now
                };
                SuncerePermission permissionPermission = new SuncerePermission()
                {
                    Name = "权限管理",
                    Type = 2,
                    Controller = "System",
                    Action = "PermissionList",
                    Order = 3,
                    ParentId = 1,
                    Status = true,
                    Static = true,
                    CreationTime = now
                };
                role.SuncerePermission.Add(systemPermission);
                role.SuncerePermission.Add(userPermission);
                role.SuncerePermission.Add(rolePermission);
                role.SuncerePermission.Add(permissionPermission);
                db.SuncereRole.Add(role);
                db.SuncerePermission.Add(systemPermission);
                db.SuncerePermission.Add(userPermission);
                db.SuncerePermission.Add(rolePermission);
                db.SuncerePermission.Add(permissionPermission);
                db.SaveChanges();
            }
        }

        [TestMethod]
        public void AirQualityPermission()
        {
            using (SuncereDataCenterModel db = new SuncereDataCenterModel())
            {
                DateTime now = DateTime.Now;
                SuncerePermission airPermission = new SuncerePermission()
                {
                    Name = "空气质量发布",
                    Type = 1,
                    Controller = "AirQuality",
                    Order = 1,
                    Icon = "air",
                    Status = true,
                    Static = true,
                    CreationTime = now
                };
                SuncerePermission hourPermission = new SuncerePermission()
                {
                    Name = "城市小时空气质量",
                    Type = 2,
                    Controller = "CityAirQuality",
                    Action = "GetCityHourlyAirQuality",
                    Order = 1,
                    ParentId = 5,
                    Status = true,
                    Static = true,
                    CreationTime = now
                };
                SuncerePermission dayPermission = new SuncerePermission()
                {
                    Name = "城市日均空气质量",
                    Type = 2,
                    Controller = "CityAirQuality",
                    Action = "GetCityDailyAirQuality",
                    Order = 2,
                    ParentId = 5,
                    Status = true,
                    Static = true,
                    CreationTime = now
                };
                SuncerePermission monthPermission = new SuncerePermission()
                {
                    Name = "城市月份空气质量",
                    Type = 2,
                    Controller = "CityAirQuality",
                    Action = "GetCityMonthlyAirQuality",
                    Order = 3,
                    ParentId = 5,
                    Status = true,
                    Static = true,
                    CreationTime = now
                };
                SuncerePermission quarterPermission = new SuncerePermission()
                {
                    Name = "城市季度空气质量",
                    Type = 2,
                    Controller = "CityAirQuality",
                    Action = "GetCityQuarterlyAirQuality",
                    Order = 4,
                    ParentId = 5,
                    Status = true,
                    Static = true,
                    CreationTime = now
                };
                SuncerePermission yearPermission = new SuncerePermission()
                {
                    Name = "城市年度空气质量",
                    Type = 2,
                    Controller = "CityAirQuality",
                    Action = "GetCityYearlyAirQuality",
                    Order = 5,
                    ParentId = 5,
                    Status = true,
                    Static = true,
                    CreationTime = now
                };
                db.SuncerePermission.Add(airPermission);
                db.SuncerePermission.Add(hourPermission);
                db.SuncerePermission.Add(dayPermission);
                db.SuncerePermission.Add(monthPermission);
                db.SuncerePermission.Add(quarterPermission);
                db.SuncerePermission.Add(yearPermission);
                db.SaveChanges();
            }
        }
    }
}
