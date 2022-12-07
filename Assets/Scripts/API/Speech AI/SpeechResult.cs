using System;

namespace API.Speech_AI
{
    [Serializable]
    public struct SpeechResult
    {
        public string greedySearchResult;
        public string beamSearchResult;
        public string command;

        public override string ToString()
        {
            return $"Greedy: {greedySearchResult}, Beam: {beamSearchResult}, Command: \"{command}\"";
        }
    }
}