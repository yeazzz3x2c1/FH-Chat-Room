# FH-Chat Room

[English](README.md) | [繁體中文](README.zh-TW.md)

# FH-Chat Room

FH-Chat Room is an educational project designed to guide students through the step-by-step implementation of real-time communication software using TCP sockets. The primary goal of this teaching material is to help students learn and apply socket programming concepts by building a simple chat application that supports text messaging and file transfer.

## Development Languages

- **Server:** C#
- **Client:** C# with WPF

## Author's Note

This project is open-sourced to provide a resource for those interested in developing real-time communication software. The `codes_records` folder includes records of the project's progress after each lesson, offering a reference for understanding the development process of a chat system.

The project is licensed under the MIT License, allowing for commercial use. However, please note that this project was developed as part of a learn-by-doing teaching approach, prioritizing simplicity and ease of understanding in the communication protocols. Encryption and other advanced security measures are not included in this project. If you intend to use this project for commercial purposes, please ensure the security and confidentiality of the communication by modifying the code accordingly.

## Project Structure

### Server
- The server-side source code is located in the `Server_Application` folder.
- To run the server:
  - Open the project in Visual Studio and execute it directly.
  - Alternatively, execute the pre-built Windows executable located at `Server_Application\Server_Application\bin\Debug\net6.0\Server.exe`.

### Client
- The client-side source code is located in the `Client_Application` folder.
- To run the client:
  - Open the project in Visual Studio and execute it directly.
  - Alternatively, execute the pre-built Windows executable located at `Client_Application\Client_Application\bin\Debug\net6.0-windows\Client_Application.exe`.

### Testing Procedure
1. Start the server.
2. Open one or more clients.
3. Enter a username on the client.
4. Begin chatting or sharing files.
5. Enjoy!

## Customization

- **Server Port:** Modify the server port by editing the `Port` variable in `Server_Application\Server_Application\Server.cs` at line 14.
- **Client Connection Address:** Modify the client connection address by editing the parameters of the `Server.Connect` method in `Client_Application\Client_Application\Server.cs` at line 42. After making changes, recompile the project.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for more details.

## Contact

**Author:** Feng-Hao, Yeh  
**Email:** zzz3x2c1@gmail.com
