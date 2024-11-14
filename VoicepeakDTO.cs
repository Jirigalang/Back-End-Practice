namespace BackEndPractice
{
    public class VoicepeakDTO
    {
        // bzd有啥用但我看别人都有
        public int Id { get; set; }

        // 想要朗读的文本
        public string? Text { get; set; }

        // 讲述人名字,现在只有一个Zundamon
        public string? NarroatorName { get; set; }

        // 情感参数格式为{情感A}={0-100},{情感B}={0-100}
        public string? Emotion {  get; set; }

        // 语速,范围为50 - 200
        public int? speed { get; set; }

        // 音高,范围为-300 - 300
        public int? pitch { get; set; }
    }
}