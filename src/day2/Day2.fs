module AdventOfCode2022.Day2

open System.IO


type value =
    | Rock
    | Paper
    | Scissors

let value_of_string (s: string) =
    match s.ToUpper() with
    | "A"
    | "X" -> Rock
    | "B"
    | "Y" -> Paper
    | "C"
    | "Z" -> Scissors
    | _ -> failwith "Invalid value"

let get_val_to_win (value: value) =
    match value with
    | Rock -> Paper
    | Paper -> Scissors
    | Scissors -> Rock


let get_val_to_lose (value: value) =
    match value with
    | Rock -> Scissors
    | Paper -> Rock
    | Scissors -> Paper

let to_chunks (make_response: string list -> value) (elems: string list) : (value * value) list =
    List.map
        (fun (line: string) ->
            line.Split(' ')
            |> Array.toList
            |> (fun f -> (value_of_string f.[0], make_response f)))
        elems


let score_of_value =
    function
    | Rock -> 1
    | Paper -> 2
    | Scissors -> 3

let get_game_result first_val second_val =
    match second_val with
    | Rock ->
        (match first_val with
         | Rock -> 3
         | Paper -> 0
         | Scissors -> 6)
    | Paper ->
        (match first_val with
         | Rock -> 6
         | Paper -> 3
         | Scissors -> 0)
    | Scissors ->
        (match first_val with
         | Rock -> 0
         | Paper -> 6
         | Scissors -> 3)


let task_1 (input: string) =
    let chunks =
        File.ReadAllLines(input)
        |> Array.toList
        |> to_chunks (fun v -> value_of_string v.[1])

    let score_of_game =
        List.fold (fun res v -> res + (get_game_result (fst v) (snd v))) 0 chunks

    let score_by_value =
        List.fold (fun res v -> snd v |> score_of_value |> (+) res) 0 chunks

    score_of_game + score_by_value

let task_2 (input: string) =
    let chunks =
        File.ReadAllLines(input)
        |> Array.toList
        |> to_chunks (fun v ->
            (match value_of_string v.[1] with
             | Rock -> value_of_string v.[0] |> get_val_to_lose
             | Paper -> value_of_string v.[0]
             | Scissors -> value_of_string v.[0] |> get_val_to_win))

    let score_of_game =
        List.fold (fun res v -> res + (get_game_result (fst v) (snd v))) 0 chunks

    let score_by_value =
        List.fold (fun res v -> snd v |> score_of_value |> (+) res) 0 chunks

    score_of_game + score_by_value
