interface ChatHandler {
    Task readComments();
    Task writeComments(string recepient, string message);
}