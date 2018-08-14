using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace YourTest.Http
{
    public class LoggerHttpMessgeHandler : DelegatingHandler
    {
        public LoggerHttpMessgeHandler(HttpMessageHandler innerHandler)
            : base(innerHandler)
        {
        }

        protected async override System.Threading.Tasks.Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            var st = new Stopwatch();
            try
            {
                st.Start();
                var res = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
                st.Stop();
                await LogResponse(res, st.Elapsed);
                return res;
            }
            catch (Exception ex)
            {
                st.Stop();
                LogEx(request, ex, st.Elapsed);
                throw ex;
            }
        }

        private async void LogEx(HttpRequestMessage request, Exception ex, TimeSpan elapsed)
        {
            var sb = new StringBuilder();
            await LogRequest(request, sb);
            LogElapsedTime(sb, elapsed);

            sb.Append("Exception:\n");

            sb.AppendLine(ex.ToString());

            Trace.WriteLine(sb.ToString());
        }

        private async Task LogResponse(HttpResponseMessage res, TimeSpan elapsed)
        {
            var sb = new StringBuilder();
            await LogRequest(res.RequestMessage, sb);

            LogElapsedTime(sb, elapsed);
            sb.AppendLine("--------------------");

            sb.AppendLine(res.ToString());
            if (res.Content != null)
            {
                try
                {
                    var canLogContent = res.Content.Headers.TryGetValues("Content-Type", out System.Collections.Generic.IEnumerable<string> values)
                        && values.Any(c => c.StartsWith("application/json", StringComparison.Ordinal));

                    var body = "";
                    if (canLogContent)
                    {
                        body = await res.Content.ReadAsStringAsync();
                    }

                    sb.AppendLine($"BODY:\n{body}");
                }
#pragma warning disable RECS0022 // A catch clause that catches System.Exception and has an empty body
                catch (Exception ex)
                {
                }
#pragma warning restore RECS0022 // A catch clause that catches System.Exception and has an empty body
            }

            sb.AppendLine("--------------------\n");


            Trace.WriteLine(sb.ToString());
        }

        private static void LogElapsedTime(StringBuilder sb, TimeSpan elapsed)
        {
            sb.AppendLine($"Respond in: {elapsed.Milliseconds}ms");
        }

        private async Task LogRequest(HttpRequestMessage request, StringBuilder sb)
        {
            sb.AppendLine("\n--------------------");
            sb.AppendLine(request.ToString());
            if (request.Content != null)
            {
                sb.AppendLine($"BODY:\n{await request.Content.ReadAsStringAsync()}");
            }
            sb.AppendLine("--------------------");
        }
    }
}
