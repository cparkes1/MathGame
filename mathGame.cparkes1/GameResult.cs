public class GameResult
{
    public int Score { get; set; }
    public TimeSpan Time {  get; set; }

    public GameResult(int score, TimeSpan time)
    {
        Score = score;
        Time = time;
    }

    public override string ToString()
    {
        return $"Score: {Score} | Time: {Time:mm\\:ss\\.fff}";
    }
}