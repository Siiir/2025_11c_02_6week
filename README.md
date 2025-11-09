# Chopirate game - 6week PJATK project

![Game Overview](./readme/GameOverview.png)
![Narrative and Objective](./readme/GameNarrativeAndObjective.png)

## Run the game

### In the browser

### Online - WIP
Will be available on GitHub Pages once teacher will allow to publish repo and make such browser release.

### Self-hosted
1. Download repo and host ./docs subfolder
```shell
git clone git@github.com:Siiir/2025_11c_02_6week.git
cd 2025_11c_02_6week/docs
python -m http.server 8000
```
2. Go to http://localhost:8000 in your browser.

### On desktop
Download release for your device. This is found in GitHub releases subpage of the repo.

## Start contributing to the repo from scratch

### Clone this repository (shell + ssh)
```shell
git clone git@github.com:Siiir/2025_11c_02_6week.git
cd 2025_11c_02_6week
```

### Install git hooks

#### Windows (shell)
**Prerequisite:** Python 3 binary is installed and accessible as `python`.
```bat
python -m venv venv/
.\venv\Scripts\pip.exe install -r requirements.txt
.\venv\Scripts\pre-commit.exe install
.\venv\Scripts\pre-commit.exe install --hook-type commit-msg
```

#### Linux/macOS (shell)

**Prerequisite:** Python 3 binary is installed and accessible as `python3`.
```shell
python3 -m venv ./venv/
./venv/bin/pip install -r requirements.txt
./venv/bin/pre-commit install
./venv/bin/pre-commit install --hook-type commit-msg
```

## Contributing

1. Clone the repository  
1.1 git clone  
1.2 Install the hooks  
2. Create a feature branch
3. Make your changes
4. Ensure all pre-commit hooks pass
5. Submit a pull request
