import speech_recognition as sr
import sys
import io

if sys.stdout is not None:
    sys.stdout = io.TextIOWrapper(sys.stdout.buffer, encoding='utf-8')
if sys.stderr is not None:
    sys.stderr = io.TextIOWrapper(sys.stderr.buffer, encoding='utf-8')
r = sr.Recognizer()
with sr.Microphone(device_index=1) as source:
    r.adjust_for_ambient_noise(source, duration=1)
    audio = r.listen(source)

try:
    text = r.recognize_google(audio, language="vi-VN")
    print(text)   # <-- in kết quả ra stdout
except sr.UnknownValueError:
    print("Không nghe rõ")
except sr.RequestError as e:
    print(f"Lỗi Google: {e}")
