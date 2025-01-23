import webbrowser

# URL para abrir no Microsoft Edge
url = "https://www.google.com"

# Caminho para o execut�vel do Edge (ajuste conforme necess�rio)
edge_path = "C:/Program Files (x86)/Microsoft/Edge/Application/msedge.exe"

# Registra o navegador Edge no webbrowser
webbrowser.register("edge", None, webbrowser.BackgroundBrowser(edge_path))

# Abre a URL no Edge
webbrowser.get("edge").open(url)
