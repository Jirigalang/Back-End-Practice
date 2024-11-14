using Microsoft.AspNetCore.Mvc;

namespace BackEndPractice.Controllers
{
    [Route("voicepeak")]
    [ApiController]
    public class VoicepeakController : Controller
    {
        [HttpPost]
        public IActionResult RunVoicepeak(VoicepeakDTO voicepeakDTO)
        {
            // voicepeak所在路径
            string command = "/home/Voicepeak/./voicepeak ";

            // 如果未指定声库则默认俊达萌
            if (string.IsNullOrEmpty(voicepeakDTO.NarroatorName))
            {
                command = AC(command, "Zundamon");
            }
            else
            {
                command = AC(command,voicepeakDTO.NarroatorName);
            }
            // 添加情感参数
            if (!string.IsNullOrEmpty(voicepeakDTO.Emotion))
            {
                command = AC(command, "--emotion");
                command = AC(command, voicepeakDTO.Emotion);
            }
            // 添加语速
            if (voicepeakDTO.speed != null)
            {
                command = AC(command, "--speed");
                command = AC(command, voicepeakDTO.Emotion);
            }
            // 添加音高
            if (voicepeakDTO.pitch != null)
            {
                command = AC(command, "--speed");
                command = AC(command, voicepeakDTO.pitch.ToString());
            }


            return Ok();
        }

        /// <summary>
        /// 将两个字符串连起来然后中间加一个空格的方法
        /// </summary>
        /// <param name="command"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        private string AC(string command, string? parameter) 
        {
            return command + " " + parameter;
        }

    }
}