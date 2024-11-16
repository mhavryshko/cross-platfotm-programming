Vagrant.configure("2") do |config|
  # Визначення IP-адрес для віртуальних машин
  hosts = {
    "linux" => "192.168.50.4",
    "mac" => "192.168.50.5",
    "windows" => "192.168.50.6"
  }

  # Linux VM
  config.vm.define "linux" do |linux|
    linux.vm.box = "ubuntu/focal64"
    linux.vm.network "private_network", ip: hosts["linux"]
    linux.vm.provider "virtualbox" do |vb|
      vb.memory = "2048"
      vb.cpus = 2
    end
    linux.vm.provision "shell", path: "provision-linux.sh"
  end

  # Windows VM
  config.vm.define "windows" do |windows|
    windows.vm.box = "gusztavvargadr/windows-10"
    windows.vm.network "private_network", ip: hosts["windows"]
    windows.vm.provider "virtualbox" do |v|
      v.name = "Windows VM"
      v.memory = "4096"
      v.cpus = 2
    end
    windows.vm.synced_folder ".", "C:/project"
    windows.vm.provision "shell", path: "provision-windows.sh"
  end

  # Mac Machine Configuration
  config.vm.define "mac" do |mac|
    mac.vm.box = "ramsey/macos-catalina"
    mac.vm.network "private_network", ip: hosts["mac"]
    mac.vm.provider "virtualbox" do |v|
      v.name = "Mac VM"
      v.memory = "4096"
      v.cpus = 2
    end
    mac.vm.synced_folder ".", "/Users/vagrant/project"
    mac.vm.provision "shell", path: "provision-mac.sh"
  end
end