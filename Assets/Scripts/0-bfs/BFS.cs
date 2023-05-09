using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

/**
 * A generic implementation of the BFS algorithm.
 * @author Erel Segal-Halevi
 * @since 2020-02
 */
public class BFS {
    public static void FindPath<NodeType>(
            IGraph<NodeType> graph, 
            NodeType startNode, NodeType endNode, 
            List<NodeType> outputPath, int maxiterations=1000)
    {
        Queue<NodeType> openQueue = new Queue<NodeType>();
        HashSet<NodeType> openSet = new HashSet<NodeType>();
        Dictionary<NodeType, NodeType> previous = new Dictionary<NodeType, NodeType>();
        openQueue.Enqueue(startNode);
        openSet.Add(startNode);
        int i; for (i = 0; i < maxiterations; ++i) { // After maxiterations, stop and return an empty path
            if (openQueue.Count == 0) {
                break;
            } else {
                NodeType searchFocus = openQueue.Dequeue();

                if (searchFocus.Equals(endNode)) {
                    // We found the target -- now construct the path:
                    outputPath.Add(endNode);
                    while (previous.ContainsKey(searchFocus)) {
                        searchFocus = previous[searchFocus];
                        outputPath.Add(searchFocus);
                    }
                    outputPath.Reverse();
                    break;
                } else {
                    // We did not found the target yet -- develop new nodes.
                    foreach (var neighbor in graph.Neighbors(searchFocus)) {
                        if (openSet.Contains(neighbor)) {
                            continue;
                        }
                        openQueue.Enqueue(neighbor);
                        openSet.Add(neighbor);
                        previous[neighbor] = searchFocus;
                    }
                }
            }
        }
    }
    public static bool checkPos<NodeType>(
            IGraph<NodeType> graph,
            NodeType startNode,
             int maxiterations = 1000)
    {
        Queue<NodeType> openQueue = new Queue<NodeType>();
        HashSet<NodeType> openSet = new HashSet<NodeType>();
        openQueue.Enqueue(startNode);
        openSet.Add(startNode);
        int i; for (i = 0; i < maxiterations; ++i)
        { // After maxiterations, stop and return an empty path
            UnityEngine.Debug.Log("checkpos i: " + i);
            if (openQueue.Count == 0)
            {
                return false;
            }
            else
            {

                NodeType searchFocus = openQueue.Dequeue();
                // We did not found the target yet -- develop new nodes.
                foreach (var neighbor in graph.Neighbors(searchFocus))
                    {
                    if (openSet.Contains(neighbor))
                        {
                            continue;
                        }
                    openQueue.Enqueue(neighbor);
                    openSet.Add(neighbor);
                }
            }
            UnityEngine.Debug.Log("checkpos for count: " + openSet.Count);
            if (openSet.Count >= 100) { return true;}

        }

        return false;
    }

    public static List<NodeType> GetPath<NodeType>(IGraph<NodeType> graph, NodeType startNode, NodeType endNode, int maxiterations=1000) {
        List<NodeType> path = new List<NodeType>();
        FindPath(graph, startNode, endNode, path, maxiterations);
        return path;
    }

}