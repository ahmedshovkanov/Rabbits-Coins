import 'dart:ui';

import 'package:flutter/cupertino.dart';

class AppConfig {
  static const String appsFlyerDevKey = 'jncU9BtLt2avbyQan3wBTY';
  static const String appsFlyerAppId = '6752892749'; // Для iOS'
  static const String bundleId = 'com.toddhale.rabcoins'; // Для iOS'
  static const String locale = 'en'; // Для iOS'
  static const String os = 'iOS'; // Для iOS'
  static const String endpoint = 'https://rabbitscoins.com'; // Для iOS'
  static const String firebaseProjectId = 'rabbits-coins'; // Для iOS'

//UI Settings
// Splash Screen
  static const Decoration splashDecoration = BoxDecoration(
    gradient: AppConfig.splashGradient,
  );

  static const Gradient splashGradient = LinearGradient(
    begin: Alignment.topCenter,
    end: Alignment.bottomCenter,
    colors: [
      Color(0xFF2AFFFF),
      Color(0xFF0D64AB),
    ],
  );
  static const Color loadingTextColor = Color(0xFFFFFFFF);
  static const Color spinerColor = Color(0xFCFFFFFF);
// Push Request Screen Settings

  static const Decoration pushRequestDecoration = BoxDecoration(
    gradient: AppConfig.pushRequestFadeGradient,
  );

  static const Gradient pushRequestGradient = LinearGradient(
    begin: Alignment.topLeft,
    end: Alignment.bottomRight,
    colors: [
      Color(0xFF2AFFFF),
      Color(0xFF0D64AB),
    ],
  );
  static const Gradient pushRequestFadeGradient = LinearGradient(
    begin: Alignment.topCenter,
    end: Alignment.bottomCenter,
    colors: [
      Color(0x00000000),
      Color.fromARGB(135, 0, 0, 0),
    ],
  );
  static const Color titleTextColor = Color(0xFFFFFFFF);
  static const Color subtitleTextColor = Color(0x80FDFDFD);

  static const Color yesButtonColor = Color(0xFFFCB301);
  static const Color yesButtonShadowColor = Color(0xA3D1710B);
  static const Color yesButtonTextColor = Color(0xFFFFFFFF);
  static const Color skipTextColor = Color(0x7DF9F9F9);

  // Путь к логотипу, если не находит добавить в pubspec.yaml
  static const String logoPath = 'assets/images/Logo.png';

  // экран ошибки подключения интернета
  // Splash Screen
  static const Decoration errorScreenDecoration = BoxDecoration(
    gradient: AppConfig.errorScreenGradient,
  );

  static const Gradient errorScreenGradient = LinearGradient(
    begin: Alignment.topCenter,
    end: Alignment.bottomCenter,
    colors: [
      Color(0xFF2AFFFF),
      Color(0xFF0D64AB),
    ],
  );
  static const Color errorScreenTextColor = Color(0xFFFFFFFF);
  static const Color errorScreenIconColor = Color(0xFCFFFFFF);

// экран загрузки WebGL
  static String webGLEndpoint =
      'https://play.unity.com/en/games/1712c4c6-d525-479c-9aa7-303fbb78c940/robbies-coins';

  static const Decoration webGLLoadingDecoration = BoxDecoration(
    gradient: AppConfig.splashGradient,
  );
  static const String webGLLoadingLogoPath = 'assets/images/Logo.png';
  static const Gradient webGLLoadingGradient = LinearGradient(
    begin: Alignment.topCenter,
    end: Alignment.bottomCenter,
    colors: [
      Color(0xFF30225C),
      Color(0xFF150B34),
    ],
  );
  static const Color webGLLoadingTextColor = Color(0xFFFFFFFF);
  static const Color webGLSpinerColor = Color(0xFCFFFFFF);
}
