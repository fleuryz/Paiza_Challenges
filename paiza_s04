using System;
using System.Collections.Generic;


class Program
{
    static int GetFewestDraws(SortedList<char, List<int>> pile_a, SortedList<char, List<int>> pile_b,
                            List<char> sentence, int first_a, int first_b, int current_char_index){
        if(current_char_index >= sentence.Count){
            //Console.WriteLine("Sentence is finished");
            return 0;
        }
        
        int draw_count_a = int.MaxValue;
        int draw_count_b = int.MaxValue;
        char current_char = sentence[current_char_index];
        //Console.WriteLine("Looking for '{0}'", current_char);
        int pila_a_index = pile_a.IndexOfKey(current_char);
        int pila_b_index = pile_b.IndexOfKey(current_char);
        if(pila_a_index != -1){
            //Console.WriteLine("On pile a");
            foreach(int current_index in pile_a.Values[pila_a_index]){
                //Console.WriteLine("Found at {0}", current_index);
                if(current_index >=  first_a){
                    //Console.WriteLine("Using it");
                    draw_count_a = 1 + current_index - first_a + GetFewestDraws(pile_a, pile_b, sentence, current_index+1, first_b, current_char_index + 1);
                    break;                    
                }
            }
        }else{
            Console.WriteLine("Not on pile aasdfg");
        }
        if(pila_b_index != -1){
            //Console.WriteLine("On pile b");
            foreach(int current_index in pile_b.Values[pila_b_index]){
                //Console.WriteLine("Found at {0}", current_index);
                if(current_index >=  first_b){
                    draw_count_b = 1 + current_index - first_b + GetFewestDraws(pile_a, pile_b, sentence, first_a, current_index+1, current_char_index + 1);
                    break;                    
                }
            }
        }else{
            //Console.WriteLine("Not on pile b");
        }
        
        if(draw_count_a < draw_count_b){
            return draw_count_a;
        }else{
            return draw_count_b;
        }
    }
    
    static int GetFewestDraws2(SortedList<char, List<int>> pile_a, SortedList<char, List<int>> pile_b,
                            List<char> sentence, int first_a, int first_b, int current_char_index){
        if(current_char_index >= sentence.Count){
            //Console.WriteLine("Sentence is finished");
            return 0;
        }
        
        int draw_count_a = int.MaxValue;
        int draw_count_b = int.MaxValue;
        int pile_a_location = -1;
        int pile_b_location = -1;
        char current_char = sentence[current_char_index];
        //Console.WriteLine("Looking for '{0}'", current_char);
        int pila_a_index = pile_a.IndexOfKey(current_char);
        int pila_b_index = pile_b.IndexOfKey(current_char);
        
        if(pila_a_index == -1 && pila_b_index == -1){
            return int.MaxValue;
        }
        
        if(pila_a_index != -1){
            //Console.WriteLine("On pile a");
            foreach(int current_index in pile_a.Values[pila_a_index]){
                //Console.WriteLine("Found at {0}", current_index);
                if(current_index >=  first_a){
                    //Console.WriteLine("Using it");
                    draw_count_a = 1 + current_index - first_a;
                    pile_a_location = current_index;
                    break;                    
                }
            }
        }else{
            //Console.WriteLine("Not on pile a");
        }
        if(pila_b_index != -1){
            //Console.WriteLine("On pile b");
            foreach(int current_index in pile_b.Values[pila_b_index]){
                //Console.WriteLine("Found at {0}", current_index);
                if(current_index >=  first_b){
                    draw_count_b = 1 + current_index - first_b;
                    pile_b_location = current_index;
                    break;                    
                }
            }
        }else{
            //Console.WriteLine("Not on pile b");
        }
        
        if(draw_count_a < draw_count_b){
            int best_draw = GetFewestDraws2(pile_a, pile_b, sentence, pile_a_location+1, first_b, current_char_index + 1);
            if(best_draw < int.MaxValue){
                return draw_count_a + best_draw;
            }else{
                if(pila_a_index != -1)
                    return draw_count_a + GetFewestDraws2(pile_a, pile_b, sentence, first_a, pile_b_location+1, current_char_index + 1);
                else
                    return int.MaxValue;
                
            }
        }else{
            int best_draw = GetFewestDraws2(pile_a, pile_b, sentence, first_a, pile_b_location+1, current_char_index + 1);
            if(best_draw < int.MaxValue){
                return draw_count_b + best_draw;
            }else{
                if(pila_b_index != -1)
                    return draw_count_b + GetFewestDraws2(pile_a, pile_b, sentence, pile_a_location+1, first_b, current_char_index + 1);
                else
                    return int.MaxValue;
            }
        }
    }
    
    static void Main()
    {
        // 自分の得意な言語で
        // Let's チャレンジ！！
        SortedList<char, List<int>> pile_a = new SortedList<char, List<int>>();
        SortedList<char, List<int>> pile_b = new SortedList<char, List<int>>();
        var sentence = new List<char>();
        var line = Console.ReadLine().Trim();
        var index = 0;
        foreach(char each_char in line){
            var lastIndex = pile_a.IndexOfKey(each_char);
            if(lastIndex == -1){
                var index_list = new List<int>();
                index_list.Add(index);
                pile_a.Add(each_char, index_list);
            }else{
                pile_a.Values[lastIndex].Add(index);
            }
            index++;
        }
        line = Console.ReadLine().Trim();
        index = 0;
        foreach(char each_char in line){
            var lastIndex = pile_b.IndexOfKey(each_char);
            if(lastIndex == -1){
                var index_list = new List<int>();
                index_list.Add(index);
                pile_b.Add(each_char, index_list);
            }else{
                pile_b.Values[lastIndex].Add(index);
            }
            index++;
        }
        line = Console.ReadLine().Trim();
        foreach(char each_char in line){
            sentence.Add(each_char);
        }
        
        int answer = GetFewestDraws2(pile_a, pile_b, sentence, 0, 0, 0);
        
        Console.WriteLine("{0}", answer);
    }   
}
