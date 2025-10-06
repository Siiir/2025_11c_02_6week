# 6week project

## Quick repo config for PJATK PCs
This will work iff Python 3 is installed and aliased as `python3`.

```bat
python -m venv venv/
.\venv\Scripts\activate.bat
pip install -r requirements.txt
pre-commit install
pre-commit install --hook-type commit-msg
```

## Quick Start

### Clone this repository (ssh version)
```shell
git clone git@github.com:Siiir/2025_11c_02_6week.git
cd 2025_11c_02_6week
```

### Create Python virtual environment

#### Linux/macOS (bash)
```bash
python3 -m venv venv/
source ./venv/bin/activate
pip install -r requirements.txt
```

#### Windows (PowerShell / Command Prompt)
```powershell
# Create virtual environment
python -m venv venv
# Install dependencies (using venv Python directly)
.\venv\Scripts\python.exe -m pip install -r requirements.txt
```

### Install the git hooks

#### Linux/macOS (bash)
```bash
source ./venv/bin/activate
pre-commit install
pre-commit install --hook-type commit-msg
```

#### Windows (Command Prompt)
```bat
venv\Scripts\activate.bat
pre-commit install
pre-commit install --hook-type commit-msg
```

#### Windows (PowerShell)
```powershell
# Install pre-commit hooks using venv executables directly
.\venv\Scripts\pre-commit.exe install
.\venv\Scripts\pre-commit.exe install --hook-type commit-msg
```

## Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Ensure all pre-commit hooks pass
5. Submit a pull request
