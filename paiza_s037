using System;
using System.Collections.Generic;

class GameResult : IEquatable<GameResult> , IComparable<GameResult>{
    public int Card { get; set; }

    public int Points { get; set; }
    
    public int GameNumber{get; set; }

    public override bool Equals(object obj)
    {
        if (obj == null) return false;
        GameResult objAsResult = obj as GameResult;
        if (objAsResult == null) return false;
        else return Equals(objAsResult);
    }
    
    public override string ToString()
    {
        return "In Game " + GameNumber + ", with card: " + Card + "   get " + Points + " points";
    }

    public int CompareTo(GameResult compareResult)
    {
          // A null value means that this object is greater.
        if (compareResult == null)
            return 1;

        else
            return this.Points.CompareTo(compareResult.Points);
    }
    
    public override int GetHashCode()
    {
        return GameNumber;
    }

    public bool Equals(GameResult other)
    {
        if (other == null) return false;
        return (this.Points.Equals(other.Points));
    }
}

class Program
{
    static int[] GetBestGame(List<int> round_cards, List<int> available_cards, int max_card, bool get_biggest_first){
        List<int> valid_numbers = new List<int>();
        List<int> excluded_numbers = new List<int>();
        var points = 0;
        
        foreach(int card in round_cards){
            points += card;
            if(valid_numbers.Contains(card)){
                valid_numbers.Remove(card);
                excluded_numbers.Add(card);
            }else if (!excluded_numbers.Contains(card)){
                valid_numbers.Add(card);
            }
        }
        var winning_card = 0;
        if(valid_numbers.Count > 0){
            valid_numbers.Sort();
            winning_card = valid_numbers[valid_numbers.Count-1];
        }
        valid_numbers.Sort();
        
        if(get_biggest_first){
            for(var i = max_card; i > winning_card;i--){
            if(excluded_numbers.Contains(i)){
                continue;
            }
            if(available_cards.Contains(i)){
                return new int[] {i, points + i};
            }
        }
        }else{
            for(var i = winning_card + 1; i <= max_card ;i++){
            if(excluded_numbers.Contains(i)){
                continue;
            }
            if(available_cards.Contains(i)){
                return new int[] {i, points + i};
            }
        }
        }
        
        
        return new int[] {available_cards[0], 0};
        
    }
    
    static int GetTotalPoints(List<int> available_cards, List<List<int>> all_round_cards, List<int> games_to_play, int max_card, bool get_biggest_first){
        var total_points = 0;
        while(available_cards.Count > 0){
            var results = new List<GameResult>();
            foreach(int game in games_to_play){
                var result = Program.GetBestGame(all_round_cards[game], available_cards, max_card, get_biggest_first);
                results.Add(new GameResult() {Card = result[0], Points = result[1], GameNumber = game});
            }
            results.Sort();
            total_points += results[results.Count-1].Points;
            available_cards.Remove(results[results.Count-1].Card);
            games_to_play.Remove(results[results.Count-1].GameNumber);
        }
        return total_points;
    }
    
    static void Main()
    {
        // 自分の得意な言語で
        // Let's チャレンジ！！
        string[] first_line = Console.ReadLine().Trim().Split(' ');
        var n_number = int.Parse(first_line[0]);
        var m_number = int.Parse(first_line[1]);
        List<List<int>> round_cards = new List<List<int>>();
        List<int> available_cards = new List<int>();
        List<int> games_to_play = new List<int>();
        
        for(var i = 0; i<m_number; i++){
            round_cards.Add(new List<int>());
            available_cards.Add(i+1);
            games_to_play.Add(i);
        }
        
        for(var i = 0; i<n_number; i++){
            string[] each_line = Console.ReadLine().Trim().Split(' ');
            for(var j = 0; j < m_number; j++){
                round_cards[j].Add(int.Parse(each_line[j]));
            }
        }
        
        
        var first_game = Program.GetTotalPoints(new List<int> (available_cards), round_cards, new List<int> (games_to_play), m_number, true);
        var second_game = Program.GetTotalPoints(new List<int> (available_cards), round_cards, new List<int> (games_to_play), m_number, false);
        
        if(first_game >second_game){
            Console.WriteLine("{0}", first_game);
        }else{
            Console.WriteLine("{0}", second_game);
        }
        
    }
}
