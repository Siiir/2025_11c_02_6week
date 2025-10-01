# 6week project

## Quick Start

### Clone this repository
```shell
https://github.com/Siiir/
git clone git@github.com:Siiir/2025_11c_02_6week.git
cd 2025_11c_02_6week
```

### Create Python3 virtual environment
```bash
python3 -m venv venv/
./venv/bin/pip install -r requirements.txt
```

### Install the git hooks (Linux script)
```bash
source ./venv/bin/activate
pre-commit install
pre-commit install --hook-type commit-msg
```

## Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Ensure all pre-commit hooks pass
5. Submit a pull request
