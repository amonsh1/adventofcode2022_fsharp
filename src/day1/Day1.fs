module AdventOfCode2022.Day1

open System.IO

let to_chunks (elems: string list) : int list =
    let rec _to_chunks (elems: string list) (chunk: int) (chunks: int list) : int list =
        match elems with
        | h :: t when h = "" -> _to_chunks t 0 (chunk :: chunks)
        | h :: t -> _to_chunks t (chunk + int (h)) chunks
        | _ -> chunks in _to_chunks elems 0 []


let get_max (elems: int list) (len: int) : int list =
    let rec _get_max (elems: int list) (max_elems: int list) : int list =
        match elems with
        | h :: t ->
            _get_max
                t
                (List.head max_elems
                 |> (fun x -> if h > x then List.tail max_elems @ [ h ] else max_elems)
                 |> List.sort)
        | _ -> max_elems in _get_max elems ((List.take len elems) |> List.map int |> List.sort)



let task_1 (input: string) =
    let chunks = File.ReadAllLines(input) |> Array.toList |> to_chunks
    (chunks |> (fun v -> get_max v 1) |> List.sum)



let task_2 (input: string) =
    let chunks = File.ReadAllLines(input) |> Array.toList |> to_chunks
    (chunks |> (fun v -> get_max v 3) |> List.sum)
