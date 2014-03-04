using System;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using KJFramework.Tracing;
using CoinAPI.CommonFramework.Enums;
using CoinAPI.Domain.Business;
using CoinAPI.Domain.Enums;
using CoinAPI.Domain.Results;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CoinAPI.Domain.Service
{
    public class RemoteApiService
    {
        #region Members     

        private static readonly ITracing _tracing = TracingManager.GetTracing(typeof(RemoteApiService));
        private Thread[] _thread;
        private static string _tracingItem;
        private static string _tracingItemCopy;
        private static object _lockObj = new object();
        private const string _dbConnectionString = "Host=192.168.1.106;Database=coin;User Id=root;password=1qaz;Charset=utf8;Convert Zero Datetime=true;Allow Zero Datetime=true;";

        #endregion

        static RemoteApiService()
        {
            Thread proc = new Thread(TracingProc);
            proc.IsBackground = true;
            proc.Name = "TracingData::TraceWatcher";
            proc.Start();
        }

        /// <summary>
        ///     从OKcoin平台获取Ticker数据
        /// </summary>
        public IExecuteResult GetBTCTickerFromOkCoin()
        {
            try
            {
                string responseText = null;
                HttpWebRequest request = (HttpWebRequest) WebRequest.Create("http://okcoin.com/api/ticker.do");
                request.Method = "Get";
                request.Headers.Clear();
                request.ContentType = "application/json; charset=UTF-8";
                request.ContentLength = 0;
                HttpWebResponse response = (HttpWebResponse) request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Stream stream = response.GetResponseStream();
                    if (stream != null)
                    {
                        StreamReader read = null;
                        using (read = new StreamReader(stream, Encoding.UTF8))
                            responseText += read.ReadToEnd();
                    }
                }
                JObject o = (JObject)JsonConvert.DeserializeObject(responseText);
                Ticker ticker = new Ticker
                {
                    BuyPrice = double.Parse(o["ticker"]["buy"].ToString()),
                    HignPrice = double.Parse(o["ticker"]["high"].ToString()),
                    LastPrice = double.Parse(o["ticker"]["last"].ToString()),
                    LowPrice = double.Parse(o["ticker"]["low"].ToString()),
                    SellPrice = double.Parse(o["ticker"]["sell"].ToString()),
                    Vol = double.Parse(o["ticker"]["vol"].ToString()), 
                };
                
                string sqlStr = string.Format("INSERT INTO `coin`.`trades`(`CoinId`,`PlatformId`,`BuyPrice`,`SellPrice`,`HighPrice`,`LowPrice`,`TradeVol`,`NowTime`,`LastPrice`)VALUE({0},{1},{2},{3},{4},{5},{6},{7},{8})",
                                    (int)CoinTypes.BTC, (int)PlatformTypes.OKCoin, ticker.BuyPrice, ticker.SellPrice, ticker.HignPrice, ticker.LowPrice, ticker.Vol,  string.Format("'{0}'",DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")) ,ticker.LastPrice);

                int count = InsertData(sqlStr);
                if (count == 0) return ExecuteResult.Fail(SystemErrors.Unknown, "插入数据库时出错");
                return ExecuteResult.Succeed(ticker);
            }
            catch (Exception ex)
            {
                _tracing.Error(ex, null);
                return ExecuteResult.Fail(SystemErrors.Unknown, ex.Message);
            }
        }

        /// <summary>
        ///     从OKcoin平台获取Ticker数据
        /// </summary>
        public IExecuteResult GetLTCTickerFromOkCoin()
        {
            try
            {
                string responseText = null;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://www.okcoin.com/api/ticker.do?symbol=ltc_cny");
                request.Method = "Get";
                request.Headers.Clear();
                request.ContentType = "application/json; charset=UTF-8";
                request.ContentLength = 0;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Stream stream = response.GetResponseStream();
                    if (stream != null)
                    {
                        StreamReader read = null;
                        using (read = new StreamReader(stream, Encoding.UTF8))
                            responseText += read.ReadToEnd();
                    }
                }
                JObject o = (JObject)JsonConvert.DeserializeObject(responseText);
                Ticker ticker = new Ticker
                {
                    BuyPrice = double.Parse(o["ticker"]["buy"].ToString()),
                    HignPrice = double.Parse(o["ticker"]["high"].ToString()),
                    LastPrice = double.Parse(o["ticker"]["last"].ToString()),
                    LowPrice = double.Parse(o["ticker"]["low"].ToString()),
                    SellPrice = double.Parse(o["ticker"]["sell"].ToString()),
                    Vol = double.Parse(o["ticker"]["vol"].ToString()),
                };

                string sqlStr = string.Format("INSERT INTO `coin`.`trades`(`CoinId`,`PlatformId`,`BuyPrice`,`SellPrice`,`HighPrice`,`LowPrice`,`TradeVol`,`NowTime`,`LastPrice`)VALUE({0},{1},{2},{3},{4},{5},{6},{7},{8})",
                                    (int)CoinTypes.LTC, (int)PlatformTypes.OKCoin, ticker.BuyPrice, ticker.SellPrice, ticker.HignPrice, ticker.LowPrice, ticker.Vol, string.Format("'{0}'", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")), ticker.LastPrice);

                int count = InsertData(sqlStr);
                if (count == 0) return ExecuteResult.Fail(SystemErrors.Unknown, "插入数据库时出错");
                return ExecuteResult.Succeed(ticker);
            }
            catch (Exception ex)
            {
                _tracing.Error(ex, null);
                return ExecuteResult.Fail(SystemErrors.Unknown, ex.Message);
            }
        }

        /// <summary>
        ///     从数据库中获取Ticker数据
        /// </summary>
        public IExecuteResult<Ticker> GetDataFromDataBase(string key)
        {
            try
            {
                KeyParse keyParse;
                KeyParse.TryParse(key, out keyParse);
                CoinTypes coinType = keyParse.CoinType;
                PlatformTypes platformType = keyParse.PlatformType;
                string sqlStr = string.Format("SELECT * FROM `coin`.`trades` WHERE `CoinId` ={0} AND `PlatformId` = {1} ORDER BY `NowTime` DESC LIMIT 1"
                    , (int)coinType, (int)platformType);
                DataTable dt = QueryData(sqlStr);
                if (dt == null || dt.Rows.Count == 0) return ExecuteResult<Ticker>.Fail(SystemErrors.Unknown, "没有获取到数据");
                Ticker ticker = new Ticker
                {
                    BuyPrice = double.Parse(dt.Rows[0]["BuyPrice"].ToString()),
                    SellPrice = double.Parse(dt.Rows[0]["SellPrice"].ToString()),
                    HignPrice = double.Parse(dt.Rows[0]["HighPrice"].ToString()),
                    LowPrice = double.Parse(dt.Rows[0]["LowPrice"].ToString()),
                    LastPrice = double.Parse(dt.Rows[0]["LastPrice"].ToString()),
                    Vol = double.Parse(dt.Rows[0]["TradeVol"].ToString())
                };
                //Tradescs tradesc = new Tradescs(coinType, ticker, platformType, ((MySqlDateTime)dt.Rows[0]["NowTime"]).GetDateTime());
                return ExecuteResult<Ticker>.Succeed(ticker);
            }
            catch (Exception ex)
            {
                _tracing.Error(ex, null);
                return ExecuteResult<Ticker>.Fail(SystemErrors.Unknown, ex.Message);
            }
        }

        /// <summary>
        ///     线程初始化
        /// </summary>
        public void Initialize()
        {
            if (_thread == null)
            {
                _thread = new Thread[2];
                _thread[0] = new Thread(GetBTCTickerFromOkCoinProc) { Name = "Thread::GetBTCData", IsBackground = true };
                _thread[0].Start();
                _thread[1] = new Thread(GetLTCTickerFromOkCoinProc) { Name = "Thread::GetLTCData", IsBackground = true };          
                _thread[1].Start();
            }
        }

        //check proc.
        private void GetBTCTickerFromOkCoinProc()
        {
            while (true)
            {
                IExecuteResult result = GetBTCTickerFromOkCoin();
                if (result.State == ExecuteResults.Succeed)
                {
                    Ticker ticker = result.GetResult<Ticker>();
                    lock (_lockObj)
                    {
                        _tracingItem = string.Format("{0}" + "   " + "  BTC" + "   " + "OKCoin" + "   " + "{1}" + "\r\n",
                            DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ticker);
                    }
                    //sleep 60s
                    Thread.Sleep(60000);
                }
            }
        }

        //check proc.
        private void GetLTCTickerFromOkCoinProc()
        {
            while (true)
            {
                IExecuteResult result = GetLTCTickerFromOkCoin();
                if (result.State == ExecuteResults.Succeed)
                {
                    Ticker ticker = result.GetResult<Ticker>();
                    lock (_lockObj)
                    {
                        _tracingItem = string.Format("{0}" + "   " + "LTC" + "   " + "OKCoin" + "   " + "{1}" + "\r\n",
                            DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ticker);
                    }
                    //sleep 60s
                    Thread.Sleep(60000);
                }
            }
        }

        //tracing proc
        private static void TracingProc()
        {
            while (true)
            {
                Thread.Sleep(1000);
                if (!Directory.Exists(ConfigurationManager.AppSettings["Log"]))
                    Directory.CreateDirectory(ConfigurationManager.AppSettings["Log"]);
                string name = Path.Combine(ConfigurationManager.AppSettings["Log"], DateTime.UtcNow.ToString("yyyyMMddHH") + ".log");
                using (FileStream file = File.Open(name, FileMode.Append, FileAccess.Write, FileShare.Read))
                using (StreamWriter writer = new StreamWriter(file, Encoding.UTF8))
                {
                    writer.AutoFlush = true;
                    lock (_lockObj)
                    {
                        if (_tracingItem != _tracingItemCopy)
                        {
                            Interlocked.Exchange(ref _tracingItemCopy, _tracingItem);
                            writer.Write(_tracingItem);
                        }
                    }
                }
            }
        }

        private int InsertData(string sqlStr)
        {
            int result;
            MySqlConnection cnn = GetConnection();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = cnn;
            cmd.CommandText = sqlStr;
            cmd.CommandType = CommandType.Text;
            try
            {
                Stopwatch watch = new Stopwatch();
                watch.Start();
                result = cmd.ExecuteNonQuery();
                watch.Stop();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
        }

        private DataTable QueryData(string sqlStr)
        {
            DataSet result = new DataSet();
            MySqlConnection cnn = GetConnection();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = cnn;
            cmd.CommandText = sqlStr;
            cmd.CommandType = CommandType.Text;
            try
            {
                Stopwatch watch = new Stopwatch();
                watch.Start();
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                adp.Fill(result);
                watch.Stop();
                return result.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
        }

        private MySqlConnection GetConnection()
        {
            MySqlConnection cnn = new MySqlConnection(_dbConnectionString);
            try
            {
                cnn.Open();
            }
            catch (Exception)
            {
                cnn.Close();
                throw;
            }
            return cnn;
        }
    }
}

