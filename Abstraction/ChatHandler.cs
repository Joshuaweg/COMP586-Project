interface ChatHandler {
    Task<List<Dictionary<string,object>>> readComments();
    Task writeComments(string recepient, string message);
}